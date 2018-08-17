
using UnityEngine;

namespace AIRogue.GameObjects
{
	class TargetingModule
	{
		private Transform thisPos;
		private Rigidbody thisRigid;
		private float speed;

		public TargetingModule(Transform targeterPosition, Rigidbody targeterRigidbody, float intercepterSpeed)
		{
			thisPos		= targeterPosition;
			thisRigid	= targeterRigidbody;
			speed		= intercepterSpeed;
		}

		public Vector3 GetIntercept(Unit unit, out float distanceToIntercept, int accuracyItterations = 3)
		{
			return GetIntercept( unit.transform.position, unit.Rigidbody.velocity, out distanceToIntercept, accuracyItterations );
		}
		private Vector3 GetIntercept(Vector3 targetPos, Vector3 targetVelocity, out float distanceToIntercept, int accuracyItterations = 3)
		{
			if (accuracyItterations < 1)
			{
				throw new System.Exception( "GetIntercept accuracy itterations must be greater than 0." );
			}

			Vector3 intercept = targetPos;
			distanceToIntercept = 0;
			float timeUntillIntercept;
			Vector3 targetTravelRelativeToTargeter;

			// in each itteration, timeUntillIntercept is updated with the new intercept position
			for (int i = 0; i < accuracyItterations; i++)
			{
				distanceToIntercept = Vector3.Distance( thisPos.position, intercept );
				timeUntillIntercept = distanceToIntercept / speed;
				targetTravelRelativeToTargeter = (targetVelocity - thisRigid.velocity) * timeUntillIntercept;
				intercept = targetPos + targetTravelRelativeToTargeter;
			}

			return intercept;
		}
	}
}
