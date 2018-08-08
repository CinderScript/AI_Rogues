
using AIRogue.GameObjects;
using UnityEngine;

namespace AIRogue.GameState.Battle.BehaviorTree
{
	class OutOfBattleBehavior : Behavior
	{
		WanderBehavior wander;
		FollowLeaderBehavior follow;
		public OutOfBattleBehavior(UnitController controller) : base( controller )
		{
			wander = new WanderBehavior( controller );
			follow = new FollowLeaderBehavior( controller );
		}
		public override RunnableBehavior EvaluateTree()
		{
			bool isLeader = ReferenceEquals( controller.Squad.LeadUnit, unit );
			if (isLeader)
			{
				return wander.EvaluateTree();
			}
			else
			{
				return follow.EvaluateTree();
			}
		}
	}
}
