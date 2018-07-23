
using AIRogue.GameObjects;
using UnityEngine;

namespace AIRogue.GameState.Battle.BehaviorTree
{
	class OutOfBattleBehavior : Behavior
	{
		WanderBehavior wander;
		public OutOfBattleBehavior(UnitController controller) : base( controller )
		{
			wander = new WanderBehavior( controller );
		}
		public override Behavior EvaluateTree()
		{
			return wander.EvaluateTree();
		}
	}
}
