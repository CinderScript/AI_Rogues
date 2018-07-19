
using AIRogue.GameObjects;
using AIRogue.GameState.Battle.Behavior;

namespace AIRogue.GameState.Battle
{
	/// <summary>
	/// The UnitController stores one Unit and applies/updates a behavior for that unit.
	/// </summary>
	abstract class UnitController {

		public Unit Unit { get; private set; }

		protected IBehavior Behavior { get; set; }

		public UnitController() { }

		/// <summary>
		/// The Initialization must be included because Squad uses a UnitController as a 
		/// Generic Type that is instanced.  Only the default constructor of a generic can be 
		/// instanced ( new () ).
		/// </summary>
		/// <param name="unit"></param>
		public virtual void AssignUnit(Unit unit)
        {
            Unit = unit;
			Unit.OnAttacked += WasAttacked;

			SetInitialBehavior();
        }
		protected abstract void SetInitialBehavior();
		protected abstract void WasAttacked(Unit attacker, float damage);

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