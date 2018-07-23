
namespace AIRogue.GameState.Battle.BehaviorTree
{
	struct UnitActions
	{
		public int Thrust { get; }
		public float Rotation { get; }

		public bool PrimaryAttack { get; }
		public bool SecondaryAttack { get; }

		public UnitActions(int thrust, float rotation, bool primaryAttack, bool secondaryAttack)
		{
			Thrust = thrust;
			Rotation = rotation;
			PrimaryAttack = primaryAttack;
			SecondaryAttack = secondaryAttack;
		}
	}
}
