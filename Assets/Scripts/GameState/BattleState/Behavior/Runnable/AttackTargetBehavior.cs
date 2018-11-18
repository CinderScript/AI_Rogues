using System.Collections.Generic;
using AIRogue.GameObjects;
using UnityEngine;

namespace AIRogue.GameState.Battle.BehaviorTree
{
	class AttackTargetBehavior : RunnableBehavior
	{
		private const float THRUST_ON_ANGLE_BELLOW = 30;
		private const float SHOOT_ON_ANGLE_BELLOW = 10;

		private Unit target;

		public AttackTargetBehavior(UnitControllerBase controller) : base( controller ) { }

		public override RunnableBehavior EvaluateTree()
		{
			target = controller.Target;
			return this;
		}
		protected override UnitActions UpdateActions()
		{
			Vector3 intercept = getIntercept();
			float distanceToIntercept = Vector3.Distance( unit.transform.position, intercept );
			float angleToIntercept = LookAngleToPosition( intercept );

			// thrust when out of range, and on target with intercept
			int thrustInput = thrust( distanceToIntercept, angleToIntercept );

			// rotate towords intercept
			int rotationInput = UnitRotationInput_LookAt( intercept );

			// fire when in range, and on target
			bool primaryAttackInput = attack(distanceToIntercept, angleToIntercept);

			return new UnitActions( thrustInput, rotationInput, primaryAttackInput, primaryAttackInput );
		}

		private Vector3 getIntercept()
		{
			Vector3 intercept = Vector3.zero;
			float distanceToIntercept;

			List<Weapon> weapons = WeaponsInRange( target.transform.position );
			if (weapons.Count > 0)
			{
				List<Vector3> intercepts = new List<Vector3>();
				foreach (var weapon in weapons)
				{
					intercepts.Add(
						weapon.TargetingModule.
						GetIntercept( target, out distanceToIntercept ) );
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
					GetIntercept( target, out distanceToIntercept );
			}

			return intercept;
		}

		private int thrust(float distanceToIntercept, float angleToIntercept)
		{
			int thrustInput = 0;
			if (distanceToIntercept > unit.WeaponWithShortestRange.Range)
			{
				if (angleToIntercept < THRUST_ON_ANGLE_BELLOW)
				{
					thrustInput = 1;
				}
			}
			return thrustInput;
		}
		private bool attack(float distanceToIntercept, float angleToIntercept)
		{
			bool primaryAttackInput = false;


			// fire if within distance and angle
			if (distanceToIntercept < unit.WeaponWithLongestRange.Range + 5)
			{
				if (angleToIntercept < SHOOT_ON_ANGLE_BELLOW)
				{
					primaryAttackInput = true;
				}
			}

			// early out if not going to attack
			if (!primaryAttackInput)
			{
				return false;
			}

			// check if ally is in the way
			List<Unit> allies = AlliesInWeaponRange();
			float[] angles = new float[allies.Count];
			float[] distance = new float[allies.Count];

			Weapon wep = unit.WeaponWithLongestRange;

			Vector3 intercept;
			float dist;
			for (int i = 0; i < allies.Count; i++)
			{
				intercept = wep.TargetingModule.GetIntercept( allies[i], out dist );
				angles[i] = LookAngleToPosition( intercept );
				distance[i] = dist;
			}

			// CANCEL fireing if angle to ally intercept is within shoot angle and in front of target
			for (int i = 0; i < angles.Length; i++)
			{
				if (angles[i] < SHOOT_ON_ANGLE_BELLOW+20)
				{
					if (distance[i] < distanceToIntercept)
					{
						primaryAttackInput = false;
					}
				}
			}

			return primaryAttackInput;
		}
	}
}