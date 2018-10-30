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

		[Header( "GameEvent Triggered Screens" )]
		public InGameLevelFinished LevelFinishedScreen = null;

		public List<Unit> EnemyUnits { get; private set; }
		public List<Unit> PlayerUnits { get; private set; }

		public GUISystem guiSystem { get; private set; }

		public LevelProgress GameProgress { get; private set; }

		void Awake()
		{
			GameProgress = LevelProgress.In_Progress;

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
			// if a win/loss has not already been triggered
			if ( GameProgress == LevelProgress.In_Progress)
			{
				// if the destroyed unit was a player unit, check for loss
				if (gameEvent.Unit.Squad.Faction == SquadFaction.Player)
				{
					if (AllPlayerDestroyed())
					{
						TriggerLoss();
					}
				}
				// if the destroyed unit was an Enemy unit, check for win
				else
				{
					if (AllEnemyDestroyed())
					{
						TriggerWin();
					}
				}
			}
		}

		void TriggerWin()
		{
			GameProgress = LevelProgress.Win;
			guiSystem.SwitchScreens( LevelFinishedScreen.GUIScreen );
		}
		void TriggerLoss()
		{
			GameProgress = LevelProgress.Loss;
			guiSystem.SwitchScreens( LevelFinishedScreen.GUIScreen );
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

		void OnDestroy()
		{
			EventManager.Instance.RemoveListener<UnitsSpawnedEvent>( OnUnitsSpawned );
			EventManager.Instance.RemoveListener<UnitDestroyedEvent>( OnUnitDestroyed );
		}
	}

	enum LevelProgress
	{
		In_Progress, Win, Loss
	}
}