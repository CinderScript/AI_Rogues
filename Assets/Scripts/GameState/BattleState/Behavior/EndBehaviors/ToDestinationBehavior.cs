
using AIRogue.GameObjects;
using UnityEngine;

namespace AIRogue.GameState.Battle.BehaviorTree
{
	class ToDestinationBehavior : Behavior
	{
		public ToDestinationBehavior(UnitController controller) : base( controller )
		{

		}

		public override Behavior EvaluateTree()
		{
			return null;
		}
	}
}
