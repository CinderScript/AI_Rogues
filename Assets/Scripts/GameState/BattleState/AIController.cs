using System.Linq;
using AIRogue.GameObjects;
using AIRogue.GameState.Battle.BehaviorTree;
using UnityEngine;

namespace AIRogue.GameState.Battle
{
	/// <summary>
	/// Implements UnitController and used by ArmyManager as a possible Generic type.
	/// This is a controller for AI units and its behaviors will not be choosen 
	/// through the GUI, but through AI logic.
	/// </summary>
	class AIController : UnitController {

		private Behavior behaviorTree;

		public override void Initialize(Unit unit, Squad squad)
		{
			base.Initialize( unit, squad );

			behaviorTree = new AIBehaviorRoot( this );
		}

		protected override RunnableBehavior SelectCurrentBehavior()
		{
			return behaviorTree.EvaluateTree();
		}
	}
}