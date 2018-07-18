using AIRogue.GameState.Battle.Behavior;
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
		protected override void setInitialBehavior()
		{
			behavior = new InputListenerBehavior( Unit );
		}
	}
}