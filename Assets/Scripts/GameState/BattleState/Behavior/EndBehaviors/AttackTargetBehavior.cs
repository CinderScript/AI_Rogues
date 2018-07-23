
using AIRogue.GameObjects;
using UnityEngine;

namespace AIRogue.GameState.Battle.BehaviorTree
{
	class AttackTargetBehavior : Behavior
	{
		public AttackTargetBehavior(UnitController controller) : base( controller ) { }

		public override Behavior EvaluateTree()
		{
			Debug.Log( "attacking" );
			return null;
		}

		public override UnitActions UpdateActions()
		{
			/// GET PLAYER CONTROLLER INPUT
			int thrustInput = (int)Input.GetAxisRaw( "Vertical" );
			float rotationInput = Input.GetAxis( "Horizontal" );

			bool primaryAttackInput = Input.GetButton( "Fire1" );
			bool secondaryAttackInput = Input.GetButton( "Fire1" );

			return new UnitActions( thrustInput, rotationInput, primaryAttackInput, secondaryAttackInput );
		}
	}
}
