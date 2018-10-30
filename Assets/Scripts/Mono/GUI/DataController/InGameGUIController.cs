using System;
using System.Collections.Generic;

using AIRogue.Events;
using AIRogue.GameObjects;
using AIRogue.GameState.Battle;
using AIRogue.Scene;

using UnityEngine;

namespace IronGrimoire.Gui.Game
{
	class InGameGUIController : MonoBehaviour
	{
		[Header( "Controller Properties" )]
		public GameSave GameSave = null;
		public UnitBank ShipLibrary = null;
		public WeaponBank WeaponLibrary = null;

		public List<Unit> EnemyUnits { get; private set; }
		public List<Unit> PlayerUnits { get; private set; }

		public GUISystem guiSystem { get; private set; }

		private bool isGameWon = false;

		void Awake()
		{
			EventManager.Instance.AddListener<UnitsSpawnedEvent>( OnUnitsSpawned );
			EventManager.Instance.AddListener<UnitDestroyedEvent>( OnUnitDestroyed );

			EnemyUnits = new List<Unit>();
			PlayerUnits = new List<Unit>();

			guiSystem = GetComponent<GUISystem>();
		}

		void OnUnitsSpawned(UnitsSpawnedEvent gameEvent)
		{
			EnemyUnits = gameEvent.EnemyUnits;
			PlayerUnits = gameEvent.PlayerUnits;
		}
		void OnUnitDestroyed(UnitDestroyedEvent gameEvent)
		{
			// if the destroyed unit was a player unit, check for loss
			if (gameEvent.Unit.Squad.Faction == SquadFaction.Player)
			{
				if ( AllPlayerDestroyed() )
				{
					TriggerLoss();
				}
			}
			// if the destroyed unit was an Enemy unit, check for win
			else
			{
				if ( AllEnemyDestroyed() )
				{
					TriggerWin();
				}
			}
		}

		void TriggerWin()
		{
			isGameWon = true;
		}
		void TriggerLoss()
		{
		}
		bool AllEnemyDestroyed()
		{
			bool allDestroyed = true;

			for (int i = 0; i < EnemyUnits.Count; i++)
			{
				if (!EnemyUnits[i].IsDestroyed)
				{
					allDestroyed = false;
					break;
				}
			}

			return allDestroyed;
		}
		bool AllPlayerDestroyed()
		{
			bool allDestroyed = true;

			for (int i = 0; i < PlayerUnits.Count; i++)
			{
				if (!PlayerUnits[i].IsDestroyed)
				{
					allDestroyed = false;
					break;
				}
			}

			return allDestroyed;
		}

		public void EndGame()
		{

		}
	}
}