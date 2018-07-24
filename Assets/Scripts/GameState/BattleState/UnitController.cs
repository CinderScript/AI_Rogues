
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

		protected RunnableBehavior Behavior;

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
			Behavior = new StartupBehavior( this );  // needed so that a reference exists when FixedUpdate is called for the first time.

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

			EventManager.Instance.AddListener<UnitDestroyedEvent>( UnitDestroyedHandler );
		}

		protected abstract RunnableBehavior SelectUnitBehavior();
		protected virtual void UpdateBehaviorSelection()
		{
			Behavior = SelectUnitBehavior();
		}

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
			UpdateBehaviorSelection();
			Behavior.CalculateActions();

			Behavior.Run_Update();
		}
		public virtual void FixedUpdate()
		{
			Behavior.Run_FixedUpdate();
		}
	}
}