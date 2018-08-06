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
		private InputListenerBehavior playerBehavior;

		public override void Initialize(Unit unit, Squad squad)
		{
			base.Initialize( unit, squad );

			playerBehavior = new InputListenerBehavior( this );
		}

		protected override RunnableBehavior SelectCurrentBehavior()
		{
			return playerBehavior;
		}
	}
}