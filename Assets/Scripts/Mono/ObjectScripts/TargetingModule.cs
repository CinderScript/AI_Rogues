using System.Collections.Generic;

using AIRogue.GameObjects;
using AIRogue.Scene;

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
			this.thisPos = targeterPosition;
			this.thisRigid = targeterRigidbody;
			this.speed = intercepterSpeed;
		}

		public Vector3 GetIntercept(Vector3 targetPos, Vector3 targetVelocity, int itterations = 2)
		{
			Vector3 intercept = targetPos;
			float timeUntillIntercept;
			Vector3 targetTravelRelativeToTargeter;

			// in each itteration, timeUntillIntercept is updated with the new intercept position
			for (int i = 0; i < itterations; i++)
			{
				timeUntillIntercept = Vector3.Distance( thisPos.position, intercept ) / speed;
				targetTravelRelativeToTargeter = (targetVelocity - thisRigid.velocity) * timeUntillIntercept;
				intercept = targetPos + targetTravelRelativeToTargeter;
			}

			return intercept;
		}
	}
}
