using UnityEngine;

namespace AIRogue.Logic.Actor
{
	/// <summary>
	/// Implements UnitController and used by Squad.  This is a controller for Player units.  
	/// This controller will run listeners for player input in addition to running AI descision making.
	/// </summary>
	class PlayerController : UnitController {

		public PlayerController()
		{
			behavior = new InputListenerBehavior( actionController );
		}
    }
}