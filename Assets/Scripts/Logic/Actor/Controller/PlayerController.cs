using UnityEngine;

namespace AIRogue.Logic.Actor
{
	/// <summary>
	/// Implements UnitController and used by Squad.  This is a controller for Player units.  
	/// This controller will run listeners for player input in addition to running AI descision making.
	/// </summary>
	class PlayerController : UnitController {

		public override void SpawnUnit(string squadName, Vector3 position)
		{
			base.SpawnUnit( squadName, position );

			behavior = new InputListenerBehavior( actionController );
		}
	}
}