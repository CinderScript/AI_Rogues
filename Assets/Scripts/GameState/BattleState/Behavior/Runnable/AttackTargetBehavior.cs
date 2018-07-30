using System.Collections.Generic;
using AIRogue.GameObjects;
using UnityEngine;

namespace AIRogue.GameState.Battle.BehaviorTree
{
	class AttackTargetBehavior : RunnableBehavior
	{
		private Transform targetTrans;
		private Rigidbody targetRigid;

		public AttackTargetBehavior(UnitController controller) : base( controller ) { }

		public override RunnableBehavior EvaluateTree()
		{
			targetTrans = controller.Target.transform;
			targetRigid = controller.Target.GetComponent<Rigidbody>();
			return this;
		}
		protected override UnitActions UpdateActions()
		{
			// rotate towords intercept
			int rotationInput = rotateToWeaponIntercept();
			// thrust when out of range
			int thrustInput = 0;
			float distanceToTarget = Vector3.Distance( unit.transform.position, targetTrans.position );
			if ( distanceToTarget > unit.WeaponWithShortestRange.Range)
			{
				thrustInput = 1;
			}

			bool primaryAttackInput = Input.GetButton( "Fire1" );
			bool secondaryAttackInput = Input.GetButton( "Fire1" );

			return new UnitActions( thrustInput, rotationInput, primaryAttackInput, secondaryAttackInput );
		}

		private int rotateToWeaponIntercept()
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

			// turn towords target
			return UnitRotationInput_LookAt( intercept );
		}
	}
}