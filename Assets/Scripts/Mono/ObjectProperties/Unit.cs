using UnityEngine;

namespace AIRogue.Logic.Actor {

	/// <summary>
	/// A gameplay unit used in Pawn of Kings.
	/// </summary>
	class Unit : MonoBehaviour
	{
		[Header( "Unit Values" )]
		public UnitType UnitType = UnitType.Not_Found;

		[Header( "Condition" )]
		public float Health = 1;
		public float Shields = 1;

		[Header( "Attack" )]
		public float AttackRange = 1;
		public float AttackDamage = 10;

		[Header( "Movement" )]
		public float MaxVelocity = 5;
		public float AccelerationForce = 3;
		public float RotationSpeed = 30;

		/* * * Assigned by Squad * * */
		public Unit Target { get; set; }
		public Squad Squad { get; set; }
	}


    public enum UnitType {
        Not_Found, TestUnit, SimpleFighter, SpaceFighter
    }
}