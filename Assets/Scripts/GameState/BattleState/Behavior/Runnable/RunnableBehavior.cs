using AIRogue.GameObjects;
using UnityEngine;

namespace AIRogue.GameState.Battle.BehaviorTree
{
	abstract class RunnableBehavior : Behavior
	{
		private UnitActions actions;

		public RunnableBehavior(UnitController unitController) : base( unitController )
		{
			actions = new UnitActions();
		}

		public void CalculateActions()
		{
			actions = UpdateActions();
		}
		public void Run_Update()
		{
			if (actions.Thrust > 0)                                // If thrusting
			{
				
			}
			else if (actions.Thrust < 0 && actions.Rotation == 0)     // If ReversTurning and not rotating
			{
				unit.ReverseTurn();
			}

			if (actions.Rotation != 0)                             // If rotating
			{
				unit.Rotate( actions.Rotation );
			}

			if (actions.PrimaryAttack)
			{
				unit.FireWeapons();
			}

			if (actions.SecondaryAttack)
			{
				unit.FireWeapons();
			}
		}
		public void Run_FixedUpdate()
		{
			if (actions.Thrust > 0)                                // If thrusting
			{
				unit.ForwardThrust();
			}
		}
		protected abstract UnitActions UpdateActions();

		protected int UnitRotationInput_LookAt(Vector3 position, float degreesOfAccuracy = 1)
		{
			int rotation = 0;
			if (LookAngleToPosition( position ) > degreesOfAccuracy)
			{
				if (DirectionToPosition( position ) == 1)
				{
					rotation = 1;
				}
				else
				{
					rotation = -1;
				}
			}

			return rotation;
		}
	}

	/// <summary>
	/// Needed due to cyclic dependancy of UnitController, RunnableBehavior, and Unit.
	/// UnitController should have it's RunnableBehavior initially set to this because 
	/// UnitController's abstract update behavior function can't be set until after 
	/// UnitController is initialized with a Unit.
	/// </summary>
	class StartupBehavior : RunnableBehavior
	{
		public StartupBehavior(UnitController unitController) : base( unitController )
		{
		}
		public override RunnableBehavior EvaluateTree()
		{
			return this;
		}
		protected override UnitActions UpdateActions()
		{
			return new UnitActions();
		}

		/// <summary>
		/// Masks the base class run so that Unit is not accessed.
		/// </summary>
		public new void Run_Update()
		{

		}
		/// <summary>
		/// Masks the base class run so that Unit is not accessed.
		/// </summary>
		public new void Run_FixedUpdate()
		{

		}
	}
}