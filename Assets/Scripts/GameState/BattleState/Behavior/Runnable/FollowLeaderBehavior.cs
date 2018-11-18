using AIRogue.GameObjects;
using UnityEngine;

namespace AIRogue.GameState.Battle.BehaviorTree
{
	class FollowLeaderBehavior : RunnableBehavior
	{
		private const float THRUST_ON_ANGLE_BELLOW = 30;
		private const float THRUST_ON_DISTANCE_ABOVE = 35;
		private const float DISTANCE_BEHIND_LEADER = 10;

		private Unit leader;
		public FollowLeaderBehavior(UnitControllerBase controller) : base( controller ) {}

		public override RunnableBehavior EvaluateTree()
		{
			leader = controller.Squad.LeadUnit;
			return this;
		}

		protected override UnitActions UpdateActions()
		{
			float dist;
			Vector3 intercept = unit.FollowModule.GetIntercept( leader, out dist, 1 );
			intercept = intercept - leader.transform.forward * DISTANCE_BEHIND_LEADER;
			int rotationInput = UnitRotationInput_LookAt( intercept );

			int thrustInput = 0;
			float distanceToLeader = Vector3.Distance( unit.transform.position, leader.transform.position );
			float angleToIntercept = LookAngleToPosition( intercept );

			if (distanceToLeader > THRUST_ON_DISTANCE_ABOVE &&
				angleToIntercept < THRUST_ON_ANGLE_BELLOW)
			{
				thrustInput = 1;
			}

			return new UnitActions( thrustInput, rotationInput, false, false );
		}
	}
}
