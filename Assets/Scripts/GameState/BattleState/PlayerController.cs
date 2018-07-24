using AIRogue.GameState.Battle.BehaviorTree;
using AIRogue.GameObjects;
using UnityEngine;

namespace AIRogue.GameState.Battle
{
	/// <summary>
	/// Implements UnitController and used by Squad.  This is a controller for Player units.  
	/// This controller will run listeners for player input in addition to running AI descision making.
	/// </summary>
	class PlayerController : UnitController
	{
		private bool behaviorSelected = false;

		protected override RunnableBehavior SelectUnitBehavior()
		{
			return new InputListenerBehavior(this);
		}
		protected override void UpdateBehaviorSelection()
		{
			if (!behaviorSelected)
			{
				base.UpdateBehaviorSelection();
				behaviorSelected = true;
			}
		}
	}
}