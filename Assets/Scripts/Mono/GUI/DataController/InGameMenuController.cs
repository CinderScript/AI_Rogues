using System.Collections.Generic;

using AIRogue.GameObjects;
using AIRogue.Persistence;
using AIRogue.Scene;

using UnityEngine;

namespace IronGrimoire.Gui.Game
{
	class InGameMenuController : MonoBehaviour
	{
		[Header( "Controller Properties" )]
		public GameSave GameSave = null;
		public UnitBank ShipLibrary = null;
		public WeaponBank WeaponLibrary = null;

		public List<Unit> EnemyUnits { get; private set; }

		
	}
}