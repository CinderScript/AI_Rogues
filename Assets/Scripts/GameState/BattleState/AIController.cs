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

		protected bool NewTargetSelected { get; set; }
		private void newTargetNotification(UnitController controller)
		{
			NewTargetSelected = true;
		}

		private Behavior behaviorTree;
		private float updateBehaviorCooldownTimer = -1;
		private const float BEHAVIOR_UPDATE_SECONDS = 0.25f;

		public override void Initialize(Unit unit, Squad squad)
		{
			base.Initialize( unit, squad );

			behaviorTree = new AIBehaviorRoot( this );
			OnTargetChosen += newTargetNotification;
		}

		protected override RunnableBehavior SelectUnitBehavior()
		{
			return behaviorTree.EvaluateTree();
		}
		protected override void UpdateBehaviorSelection()
		{
			if (updateBehaviorCooldownTimer < 0 )
			{
				base.UpdateBehaviorSelection();
				updateBehaviorCooldownTimer = BEHAVIOR_UPDATE_SECONDS;
			}
		}

		public override void Update()
		{
			updateBehaviorCooldownTimer -= Time.deltaTime;

			base.Update(); // updates selected behavior
		}
	}
}