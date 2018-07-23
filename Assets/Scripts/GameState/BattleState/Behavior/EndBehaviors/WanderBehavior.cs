using AIRogue.GameObjects;
using UnityEngine;

namespace AIRogue.GameState.Battle.BehaviorTree
{
	class WanderBehavior : Behavior
	{
		public WanderBehavior(UnitController controller) : base( controller )
		{

		}

		public override Behavior EvaluateTree()
		{
			return this;
		}
	}
}
