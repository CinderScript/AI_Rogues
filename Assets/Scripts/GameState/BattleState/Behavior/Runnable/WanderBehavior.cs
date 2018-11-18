using AIRogue.GameObjects;
using UnityEngine;

namespace AIRogue.GameState.Battle.BehaviorTree
{
	class WanderBehavior : RunnableBehavior
	{
		public WanderBehavior(UnitControllerBase controller) : base( controller )
		{

		}

		public override RunnableBehavior EvaluateTree()
		{
			return this;
		}

		protected override UnitActions UpdateActions()
		{
			return new UnitActions();
		}
	}
}
