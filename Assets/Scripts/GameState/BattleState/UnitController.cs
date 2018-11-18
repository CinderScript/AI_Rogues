using AIRogue.GameState.Battle.BehaviorTree;
using AIRogue.GameObjects;
using UnityEngine;
using AIRogue.Events;

namespace AIRogue.GameState.Battle
{
	/// <summary>
	/// Implements UnitControllerBase and used by Squad.
	/// </summary>
	class UnitController : UnitControllerBase
	{
		private Behavior playerBehavior;
		private Behavior aiBehaviorTree;
		private Behavior pausedUnits;

		private bool unitsPaused = true;
		private bool isSelected = false;

		public override void Initialize(Unit unit, Squad squad)
		{
			base.Initialize( unit, squad );

			playerBehavior = new InputListenerBehavior( this );
			aiBehaviorTree = new AIBehaviorRoot( this );
			pausedUnits = new EmptyBehavior( this );

			unitsPaused = true; // units start paused so they can't move during opening
			EventManager.Instance.AddListenerOnce<MatchStartEvent>( OnStartMatch );
			EventManager.Instance.AddListenerOnce<MatchFinishedEvent>( OnMatchFinished );
			EventManager.Instance.AddListener<UnitSelectedEvent>( OnUnitSelectionChange );
		}

		protected override RunnableBehavior SelectCurrentBehavior()
		{
			if (unitsPaused)
			{
				return pausedUnits.EvaluateTree();
			}
			else
			{
				if (isSelected)
				{
					return playerBehavior.EvaluateTree();
				}
				else
				{
					return aiBehaviorTree.EvaluateTree();
				}
			}
		}

		void OnStartMatch(MatchStartEvent gameEvent)
		{
			unitsPaused = false;
		}
		void OnMatchFinished(MatchFinishedEvent gameEvent)
		{
			unitsPaused = true;
		}
		void OnUnitSelectionChange(UnitSelectedEvent gameEvent)
		{
			if (gameEvent.SelectedUnit == Unit)
			{
				isSelected = true;
			}
			else
			{
				isSelected = false;
			}
		}
	}
}