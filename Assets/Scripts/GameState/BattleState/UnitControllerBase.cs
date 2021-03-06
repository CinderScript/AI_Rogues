

using AIRogue.Events;
using AIRogue.GameObjects;
using AIRogue.GameState.Battle.BehaviorTree;
using UnityEngine;

namespace AIRogue.GameState.Battle
{
	/// <summary>
	/// The UnitController stores one Unit and applies/updates a behavior for that unit.
	/// </summary>
	abstract class UnitControllerBase
	{
		public Unit Unit { get; private set; }
		public Squad Squad { get; private set; }

		private Unit _target;
		public Unit Target
		{
			get {
				return _target;
			}
			private set {
				if (_target != null)
				{
					_target.OnUnitDestroyed -= targetDestroyedHandler;
				}

				_target = value;

				if (_target != null)
				{
					_target.OnUnitDestroyed += targetDestroyedHandler;
				}
			}
		}

		public bool InCombat { get; set; }

		protected RunnableBehavior Behavior;
		private float updateBehaviorCooldownTimer = -1;
		private readonly float updateCooldownDuration = 0.20f + Random.Range(0, 0.06f); // so all units don't update at once

		public UnitControllerBase()
		{
			InCombat = false;
		}

		/// <summary>
		/// The Initialization must be included because Squad uses a UnitController as a 
		/// Generic Type that is instanced.  Only the default constructor of a generic can be 
		/// instanced ( new () ).
		/// </summary>
		/// <param name="unit"></param>
		public virtual void Initialize(Unit unit, Squad squad)
		{
			Unit = unit;
			Squad = squad;
			Behavior = new EmptyBehavior( this );  // needed so that a reference exists when FixedUpdate is called for the first time.

			Unit.OnUnitDestroyed += thisUnitDestroyedHandler;
		}

		protected abstract RunnableBehavior SelectCurrentBehavior();
		private void updateBehaviorSelection()
		{
			Behavior = SelectCurrentBehavior();
		}

		private void thisUnitDestroyedHandler(Unit unit)
		{
			Squad.UnitDestroyed( this );
		}
		private void targetDestroyedHandler(Unit unit)
		{
			Target = null;
			updateBehaviorSelection();
		}

		public virtual void Update()
		{
			updateBehaviorCooldownTimer -= Time.deltaTime;

			if (updateBehaviorCooldownTimer < 0)
			{
				updateBehaviorSelection();
				updateBehaviorCooldownTimer = updateCooldownDuration;
			}

			if (Target == null)
			{
				UpdateTarget();
			}

			Behavior.CalculateActions();
			Behavior.Run_Update();
		}
		public virtual void FixedUpdate()
		{
			Behavior.Run_FixedUpdate();
		}

		public void UpdateTarget()
		{
			// check for engaged units
			// check for unit attacking me
			// check for closest
			// check for hostile squads in range and engage

			Target = closestHostileUnit();
		}

		private float distanceToUnit(Unit unit)
		{
			return Vector3.Distance( Unit.transform.position, unit.transform.position );
		}
		//private List<Unit> getUnitsTargetingMe()
		//{
		//	List<Unit> targetingMe = new List<Unit>();

		//	foreach (var squad in Squad.EngagedSquads)
		//	{
		//		foreach (var controller in squad.Controllers)
		//		{
		//			if (ReferenceEquals( controller.Target, Unit ))
		//			{
		//				targetingMe.Add( controller.Unit );
		//			}
		//		}
		//	}

		//	return targetingMe;
		//}
		private Unit closestHostileUnit()
		{
			Unit attacker = null;

			// find closest engaged unit
			float shortest = float.MaxValue;
			foreach (var squad in Squad.EngagedSquads)
			{
				foreach (var controller in squad.Controllers)
				{
					float distance = distanceToUnit( controller.Unit );
					if (distance < shortest)
					{
						shortest = distance;
						attacker = controller.Unit;
					}
				}
			}

			return attacker;
		}
	}
}