
using System.Collections.Generic;
using AIRogue.GameObjects;
using AIRogue.GameState.Battle.Behavior;

namespace AIRogue.GameState.Battle
{
	/// <summary>
	/// The UnitController stores one Unit and applies/updates a behavior for that unit.
	/// </summary>
	abstract class UnitController {

		protected Unit Unit { get; private set; }
		protected List<UnitController> squadControllers;

		protected IBehavior Behavior;
		protected Dictionary<Unit, float> Attackers 
			= new Dictionary<Unit, float>(new General.ReferenceEqualityComparer<Unit>());

		public UnitController() { }

		/// <summary>
		/// The Initialization must be included because Squad uses a UnitController as a 
		/// Generic Type that is instanced.  Only the default constructor of a generic can be 
		/// instanced ( new () ).
		/// </summary>
		/// <param name="unit"></param>
		public virtual void Initialize(Unit unit, List<UnitController> squadControllers)
        {
            Unit = unit;
			Unit.OnAttacked += WasAttacked;
			this.squadControllers = squadControllers;

			SetInitialBehavior();
        }
		protected abstract void SetInitialBehavior();

		protected virtual void WasAttacked(Unit attacker, float damage)
		{
			float totalDamage;
			if (Attackers.TryGetValue(attacker, out totalDamage))
			{
				Attackers[attacker] = damage + totalDamage;
			}
			else
			{
				Attackers[attacker] = damage;
			}

		}

		public virtual void Update()
		{
			Behavior.Update();
		}
		public virtual void FixedUpdate()
		{
			Behavior.FixedUpdate();
		}
	}
}