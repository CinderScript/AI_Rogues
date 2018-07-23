
using System;
using System.Collections.Generic;
using System.Linq;
using AIRogue.Events;
using AIRogue.GameObjects;
using AIRogue.GameState.Battle.BehaviorTree;

namespace AIRogue.GameState.Battle
{
	/// <summary>
	/// The UnitController stores one Unit and applies/updates a behavior for that unit.
	/// </summary>
	abstract class UnitController {

		public Unit Unit { get; private set; }
		protected Squad Squad { get; private set; }

		protected Behavior Behavior;
		private UnitActions actions;

		private HashSet<Unit> attackers
			= new HashSet<Unit>( 
				new General.ReferenceEqualityComparer<Unit>() );
		protected Unit[] Attackers = new Unit[0];

		protected HashSet<UnitController> alliesWithTargets
			= new HashSet<UnitController>( 
				new General.ReferenceEqualityComparer<UnitController>() );
		protected UnitController[] AlliesWithTargets = new UnitController[0];

		/* * * Environment State * * */
		private Unit target;
		public Unit Target {
			get { return target; }
			protected set {
				target = value;
				if (value != null)
				{
					OnTargetChosen?.Invoke( this );
				}
			}
		}

		public delegate void TargetChosen(UnitController targetChooser);
		public TargetChosen OnTargetChosen;

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

			Behavior = GetUnitBehavior();

			EventManager.Instance.AddListener<UnitDestroyedEvent>( UnitDestroyedHandler );
		}

		protected abstract Behavior GetUnitBehavior();

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
			alliesWithTargets.Add( ally );
			AlliesWithTargets = alliesWithTargets.ToArray();
		}
		private void UnitDestroyedHandler(UnitDestroyedEvent gameEvent)
		{
			if ( ReferenceEquals( gameEvent.Unit, this.Unit ) )
			{
				Squad.controllers.Remove( this );
			}
		}

		public virtual void Update()
		{
			Behavior = GetUnitBehavior();
			actions = Behavior.UpdateActions();

			if (actions.Thrust > 0)                                // If thrusting
			{
				Unit.ForwardThrust();
			}
			else if (actions.Thrust < 0 && actions.Rotation == 0)     // If ReversTurning and not rotating
			{
				Unit.ReverseTurn();
			}

			if (actions.Rotation != 0)                             // If rotating
			{
				Unit.Rotate( actions.Rotation );
			}

			if (actions.PrimaryAttack)
			{
				Unit.FireWeapons();
			}

			if (actions.SecondaryAttack)
			{
				Unit.FireWeapons();
			}
		}
		public virtual void FixedUpdate()
		{

		}
	}
}