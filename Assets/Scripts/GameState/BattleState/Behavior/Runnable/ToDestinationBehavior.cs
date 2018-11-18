
using AIRogue.GameObjects;
using UnityEngine;

namespace AIRogue.GameState.Battle.BehaviorTree
{
	class ToDestinationBehavior : RunnableBehavior
	{
		public ToDestinationBehavior(UnitControllerBase controller) : base( controller )
		{

		}

		public override RunnableBehavior EvaluateTree()
		{
			return null;
		}

		protected override UnitActions UpdateActions()
		{
			throw new System.NotImplementedException();
		}
	}
}
