using System.Collections.Generic;
using AIRogue.GameObjects;
using UnityEngine;

namespace AIRogue.GameState.Battle.BehaviorTree
{
	class AttackTargetBehavior : RunnableBehavior
	{
		private const float THRUST_ON_ANGLE_BELLOW = 30;
		private const float SHOOT_ON_ANGLE_BELLOW = 10;

		private Transform targetTrans;
		private Rigidbody targetRigid;

		public AttackTargetBehavior(UnitController controller) : base( controller ) { }

		public override RunnableBehavior EvaluateTree()
		{
			targetTrans = controller.Target.transform;
			targetRigid = controller.Target.Rigidbody;
			return this;
		}
		protected override UnitActions UpdateActions()
		{
			Vector3 intercept = getIntercept();
			float distanceToIntercept = Vector3.Distance( unit.transform.position, intercept );
			float angleToIntercept = LookAngleToPosition( intercept );

			// rotate towords intercept
			int rotationInput = UnitRotationInput_LookAt( intercept );

			// thrust when out of range, and on target with intercept
			int thrustInput = 0;
			if (distanceToIntercept > unit.WeaponWithShortestRange.Range)
			{
				if ( angleToIntercept < THRUST_ON_ANGLE_BELLOW )
				{
					thrustInput = 1;
				}
			}

			// fire when in range, and on target
			bool primaryAttackInput = false;
			if (distanceToIntercept < unit.WeaponWithLongestRange.Range+5)
			{
				if (angleToIntercept < SHOOT_ON_ANGLE_BELLOW)
				{
					primaryAttackInput = true;
				}
			}

			return new UnitActions( thrustInput, rotationInput, primaryAttackInput, primaryAttackInput );
		}

		private Vector3 getIntercept()
		{
			Vector3 intercept = Vector3.zero;

			List<Weapon> weapons = WeaponsInRange( targetTrans.position );
			if (weapons.Count > 0)
			{
				List<Vector3> intercepts = new List<Vector3>();
				foreach (var weapon in weapons)
				{
					intercepts.Add(
						weapon.TargetingModule.
						GetIntercept( targetTrans.position, targetRigid.velocity ) );
				}

				foreach (var weapIntercept in intercepts)
				{
					intercept += weapIntercept;
				}
				intercept /= intercepts.Count;
			}
			else
			{
				intercept = unit.WeaponWithLongestRange.TargetingModule.
					GetIntercept( targetTrans.position, targetRigid.velocity );
			}

			return intercept;
		}
	}
}