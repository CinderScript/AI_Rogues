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
		public LevelProperties LevelProperties = null;

		[Header( "GameEvent Triggered Screens" )]
		public GUIScreen MatchEndedScreen = null;

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
						TriggerRoundEnding( LevelProgress.Loss );
					}
				}
				// if the destroyed unit was an Enemy unit, check for win
				else
				{
					if (AllEnemyDestroyed())
					{
						TriggerRoundEnding( LevelProgress.Win );
					}
				}
			}
		}

		void TriggerRoundEnding(LevelProgress result)
		{
			GameProgress = result;
			TimeManager.Instance.SetGameplaySpeed( 0.25f );
			guiSystem.SwitchScreens( MatchEndedScreen );
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

		public int GetEnemyDestroyedCount()
		{
			int destroyed = 0;
			foreach (var unit in EnemyUnits)
			{
				if (unit.IsDestroyed)
				{
					destroyed++;
				}
			}

			return destroyed;
		}
		public int GetPlayerDestroyedCount()
		{
			int destroyed = 0;
			foreach (var unit in PlayerUnits)
			{
				if (unit.IsDestroyed)
				{
					destroyed++;
				}
			}

			return destroyed;
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