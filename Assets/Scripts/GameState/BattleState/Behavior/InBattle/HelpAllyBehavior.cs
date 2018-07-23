
using AIRogue.GameObjects;
using UnityEngine;

namespace AIRogue.GameState.Battle.BehaviorTree
{
	class HelpAllyBehavior : Behavior
	{
		public HelpAllyBehavior(UnitController controller) : base( controller ) { }

		public override Behavior EvaluateTree()
		{
			return this;
		}
	}
}
