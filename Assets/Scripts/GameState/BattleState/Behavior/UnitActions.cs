
namespace AIRogue.GameState.Battle.BehaviorTree
{
	struct UnitActions
	{
		public const int RIGHT = 1;
		public const int LEFT = -1;

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

		//public UnitActions(int thrust, UnitRotationInput rotation, bool primaryAttack, bool secondaryAttack)
		//{
		//	Thrust = thrust;
		//	Rotation = rotation.Rotation;
		//	PrimaryAttack = primaryAttack;
		//	SecondaryAttack = secondaryAttack;
		//}
	}

	//struct UnitRotationInput
	//{
	//	public float Rotation { get; }
	//	public UnitRotationInput(float rotation)
	//	{
	//		Rotation = rotation;
	//	}
	//}
}
