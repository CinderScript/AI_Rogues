using System.Collections.Generic;

using AIRogue.Events;
using AIRogue.GameObjects;
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
		public List<Unit> PlayerUnits { get; private set; }

		void Awake()
		{
			EventManager.Instance.AddListener<UnitsSpawnedEvent>( OnUnitsSpawned );

			EnemyUnits = new List<Unit>();
			PlayerUnits = new List<Unit>();
		}

		void OnUnitsSpawned(UnitsSpawnedEvent gameEvent)
		{
			EnemyUnits = gameEvent.EnemyUnits;
			PlayerUnits = gameEvent.PlayerUnits;
		}
	}
}