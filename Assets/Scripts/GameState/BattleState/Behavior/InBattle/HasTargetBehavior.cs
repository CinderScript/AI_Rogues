
using AIRogue.GameObjects;
using UnityEngine;

namespace AIRogue.GameState.Battle.BehaviorTree
{
	class HasTargetBehavior : Behavior
	{
		public HasTargetBehavior(UnitController controller) : base( controller )
		{

		}

		public override RunnableBehavior EvaluateTree()
		{
			throw new System.NotImplementedException();
		}
	}
}
