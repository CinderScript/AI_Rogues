using AIRogue.GameObjects;

namespace AIRogue.GameState.Battle.BehaviorTree
{
	abstract class RunnableBehavior : Behavior
	{
		private UnitActions actions;
		private Unit unit;

		public RunnableBehavior(UnitController unitController) : base( unitController )
		{
			actions = new UnitActions();
			unit = unitController.Unit;
		}

		public void CalculateActions()
		{
			actions = UpdateActions();
		}
		public void Run_Update()
		{
			if (actions.Thrust > 0)                                // If thrusting
			{
				unit.ForwardThrust();
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

		}

		protected abstract UnitActions UpdateActions();
	}
}