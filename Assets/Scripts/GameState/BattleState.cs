using System.Collections.Generic;
using System.IO;
using AIRogue.Events;
using AIRogue.GameObjects;
using AIRogue.GameState.Battle;
using AIRogue.Scene;
using IronGrimoire.Persistence;
using UnityEngine;

namespace AIRogue.GameState
{
	/// <summary>
	/// Inherits IGameState.  The state that controls the gameplay during a battle.
	/// 
	/// Creates a list of Squads populating them with Units and UnitControllers then updates each squad
	/// during gameplay.
	/// 
	/// DEPENDENCY:  GameStateManager has a dependancy for BattleState.
	/// BattleState has a dependancy for Squad, AIController, and PlayerController.
	/// 
	/// Temp dependncy on Grid for character spawning.  Will be handled by spawner or event system 
	/// in future.
	/// </summary>
	class BattleState : IGameState {

		private readonly List<Squad> squads;
		
		/// <summary>
		/// Instances a new BattleState creating a list of Squad objects for player and AI.
		/// </summary>
		/// <param name="unitBank"></param>
		/// <param name="levelProperties"></param>
        public BattleState(BattleStateDriver driver)
        {
			UnitBank unitBank = driver.UnitLibrary;
			WeaponBank weaponBank = driver.WeaponLibrary;
			LevelProperties levelProperties = driver.LevelProperties;
			GameSave gameSave = driver.GameSave;

			/* Populate list of Squads
			 * Initialize each Squad with the correct UnitController
			 * Give each Squad a reference to...
			 */

			squads = new List<Squad>();

			/* Add Squads to List
			 * create a new Squad and then add units to that squad.
			 * A UnitController Type is defined as the Generics value when 
			 * a unit is added.  This controller instanced and run for the 
			 * unit specified.
			 */

			/* ADD PLAYER SQUADS */
			Squad playerSquad = new Squad( squads, levelProperties.PlayerStart.position, "Player", SquadFaction.Player );
			squads.Add( playerSquad );

			foreach (var playerUnit in gameSave.Squad)
			{
				var unitPrefab = unitBank.GetPrefab( playerUnit.UnitModel );
				var unit = playerSquad.SpawnUnit( unitPrefab );

				foreach (var weapon in playerUnit.Weapons)
				{
					unit.SpawnWeapon( weaponBank.GetPrefab( weapon ) );
				}
			}

			EventManager.Instance.QueueEvent( new PlayerLeaderChangedEvent( playerSquad.LeadUnit ) );

			//// player 
			//for (int i = 0; i < 2; i++)
			//{
			//	unitPrefab = unitBank.GetPrefab( UnitModel.Test_Unit );
			//	player = playerSquad.SpawnUnit( unitPrefab );
			//	player.SpawnWeapon( weaponBank.GetPrefab( WeaponModel.Blue_Cannon ) );
			//	player.SpawnWeapon( weaponBank.GetPrefab( WeaponModel.Red_Cannon ) );
			//}

			//// player 
			//for (int i = 0; i < 4; i++)
			//{
			//	unitPrefab = unitBank.GetPrefab( UnitModel.Simple_Fighter );
			//	Unit player2 = playerSquad.SpawnUnit( unitPrefab );
			//	player2.SpawnWeapon( weaponBank.GetPrefab( WeaponModel.Red_Laser ) );
			//	player2.SpawnWeapon( weaponBank.GetPrefab( WeaponModel.Blue_Cannon ) );
			//}

			/* ADD ENEMY SQUADS */
			for (int i = 0; i < levelProperties.AIStart.Count; i++)
			{				
				Squad aiSquad = new Squad( squads, levelProperties.AIStart[i].position, "AISquad-" + i, SquadFaction.AI_1 );
				squads.Add( aiSquad );

				// add enemies for this squad
				var levelRank = levelProperties.EnemyRank;
				var enemyPrefabs = GetEnemies( levelRank, levelProperties.EnemyCount, unitBank );
				foreach (var enemyPrefab in enemyPrefabs)
				{
					Unit enemyUnit = aiSquad.SpawnUnit( enemyPrefab );


					// spawn weapons for this enemy using level's rank, not enemy's
					var count = enemyUnit.WeaponMountCount;
					var enemyWeapons = GetEnemyWeapons( levelRank, count, weaponBank );
					foreach (var weaponPrefab in enemyWeapons)
					{
						enemyUnit.SpawnWeapon( weaponPrefab );
					}
				}
			}

			EventManager.Instance.QueueEvent( new UnitsSpawnedEvent( squads ) );
			EventManager.Instance.QueueEvent( new BattleStateStartEvent() );
		}

		/// <summary>
		/// Runs the correct Army's update loop and keeps track of turns.
		/// </summary>
		public void Update()
        {
			foreach (var squad in squads)
			{
				squad.Update();
			}
		}
		public void FixedUpdate()
		{
			foreach (var squad in squads)
			{
				squad.FixedUpdate();
			}
		}


		private List<GameObject> GetEnemies(int enemyRank, int count, UnitBank unitBank)
		{
			// make enemy rank that is equal to level difficulty count for 1 unit.
			// 1 rank below level difficulty equals 0.5 units. (add two)
			// 1 rank above equals 2 units.


			List<GameObject> enemyPrefabs = new List<GameObject>();
			for (int i = 0; i < count; i++)
			{
				// if not on last count, pick rank between +-1 of level difficulty
				int maxRank = enemyRank;
				if (i + 1 < count)
				{
					maxRank += 1;

					//this unit counts for 2
					i++;
				}

				int minRank = enemyRank - 1;
				// make sure min rank is not less than 1
				minRank = minRank < 1 ? 1 : minRank;

				// random int is exclusive on max (make +1 to include maxRank)
				int randomRank = Random.Range( minRank, maxRank + 1 );

				var unitPrefabs = unitBank.GetUnitPrefabsByRank( randomRank );
				int rankFound = unitPrefabs[0].GetComponent<Unit>().Rank;

				// randomly select one of the prefabs
				int randomIndex = Random.Range( 0, unitPrefabs.Count );
				enemyPrefabs.Add( unitPrefabs[randomIndex] );

				// if using lower rank enemy, add another
				if (rankFound < enemyRank)
				{
					randomIndex = Random.Range( 0, unitPrefabs.Count );
					enemyPrefabs.Add( unitPrefabs[randomIndex] );
				}
			}

			return enemyPrefabs;
		}
		private List<GameObject> GetEnemyWeapons(int rank, int count, WeaponBank weaponBank)
		{
			// randomly select a weapon +-1 rank
			var minRank = rank - 1;
			var maxRank = rank + 1;
			minRank = minRank < 1 ? 1 : minRank;

			List<GameObject> weaponPrefabs = new List<GameObject>();
			for (int i = 0; i < count; i++)
			{
				// random int is exclusive on max (make +1 to include maxRank)
				int randomRank = Random.Range( minRank, maxRank + 1 );

				var unitPrefabs = weaponBank.GetWeaponPrefabsByRank( randomRank );

				// randomly select one of the prefabs
				int randomWeap = Random.Range( 0, unitPrefabs.Count );
				weaponPrefabs.Add( unitPrefabs[randomWeap] );
			}

			return weaponPrefabs;
		}
	}
}