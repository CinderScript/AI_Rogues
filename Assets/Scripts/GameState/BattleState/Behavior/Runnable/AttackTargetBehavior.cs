
using AIRogue.GameObjects;
using UnityEngine;

namespace AIRogue.GameState.Battle.BehaviorTree
{
	class AttackTargetBehavior : RunnableBehavior
	{
		private Transform target;

		public AttackTargetBehavior(UnitController controller) : base( controller ) { }

		public override RunnableBehavior EvaluateTree()
		{
			target = controller.Target.transform;
			return this;
		}

		protected override UnitActions UpdateActions()
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
