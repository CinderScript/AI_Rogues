
using System.Collections.Generic;
using System.Linq;
using AIRogue.GameObjects;
using AIRogue.GameState.Battle.Behavior;

namespace AIRogue.GameState.Battle
{
	/// <summary>
	/// The UnitController stores one Unit and applies/updates a behavior for that unit.
	/// </summary>
	abstract class UnitController {

		protected Unit Unit { get; private set; }
		protected Squad Squad { get; private set; }

		protected Behavior.Behavior Behavior;
		private HashSet<Unit> attackers
			= new HashSet<Unit>( 
				new General.ReferenceEqualityComparer<Unit>() );
		protected Unit[] Attackers = new Unit[0];

		protected HashSet<UnitController> AlliesWithTargets
			= new HashSet<UnitController>( 
				new General.ReferenceEqualityComparer<UnitController>() );

		public delegate void TargetChosen(UnitController attacker);
		public TargetChosen OnTargetChosen;

		/* * * Environment State * * */
		public virtual bool HasAttacker
		{
			get {
				return Attackers.Length > 0;
			}
		}
		private Unit target;
		public Unit Target {
			get { return target; }
			protected set {
				target = value;
				OnTargetChosen?.Invoke( this );
			}
		}

		public UnitController() { }

		/// <summary>
		/// The Initialization must be included because Squad uses a UnitController as a 
		/// Generic Type that is instanced.  Only the default constructor of a generic can be 
		/// instanced ( new () ).
		/// </summary>
		/// <param name="unit"></param>
		public virtual void Initialize(Unit unit, Squad squad)
        {
            Unit = unit;
			Unit.OnDamageTaken += TookDamage;
			Squad = squad;

			/// Set each OnTargetChosen delegate with each controller's <see cref="AllyChoseTarget"/> method
			foreach (var controller in Squad.controllers)
			{
				// should always be true if initialized before being added to squad
				if (!ReferenceEquals(this, controller)) 
				{
					// add other controller's events to this delegate
					OnTargetChosen += controller.AllyChoseTarget;
					controller.OnTargetChosen += AllyChoseTarget;
				}
			}

			SetInitialBehavior();
        }

		protected abstract void SetInitialBehavior();

		protected virtual void TookDamage(Unit attacker, float damage)
		{
			//float totalDamage;
			//if (Attackers.TryGetValue(attacker, out totalDamage))
			//{
			//	Attackers[attacker] = damage + totalDamage;
			//}
			//else
			//{
			//	Attackers[attacker] = damage;
			//}

			if ( ReferenceEquals(attacker.Squad, Squad) )
			{
				// damaged by alli
			}
			else if (!attackers.Contains( attacker ))
			{
				attackers.Add( attacker );
				Attackers = attackers.ToArray();
			}
		}
		protected virtual void AllyChoseTarget(UnitController ally)
		{
			UnityEngine.Debug.Log( $"{Unit}'s ally {ally.Unit} just selected target {ally.target}" );
			if (!AlliesWithTargets.Contains( ally ))
			{
				AlliesWithTargets.Add( ally );
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