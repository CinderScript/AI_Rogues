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
        public BattleState(UnitBank unitBank, WeaponBank weaponBank, LevelProperties levelProperties)
        {

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

			GameObject unitPrefab;
			Unit player;
			unitPrefab = unitBank.GetPrefab( UnitModel.Simple_Fighter );
			player = playerSquad.SpawnUnit<UnitController>( unitPrefab );
			player.SpawnWeapon( weaponBank.GetPrefab(WeaponModel.Green_Laser) );
			player.SpawnWeapon( weaponBank.GetPrefab(WeaponModel.Red_Laser) );

			EventManager.Instance.QueueEvent( new UnitSelectedEvent( player ) );

			// player 
			for (int i = 0; i < 2; i++)
			{
				unitPrefab = unitBank.GetPrefab( UnitModel.Test_Unit );
				player = playerSquad.SpawnUnit<UnitController>( unitPrefab );
				player.SpawnWeapon( weaponBank.GetPrefab( WeaponModel.Blue_Cannon ) );
				player.SpawnWeapon( weaponBank.GetPrefab( WeaponModel.Red_Cannon ) );
			}

			// player 
			for (int i = 0; i < 4; i++)
			{
				unitPrefab = unitBank.GetPrefab( UnitModel.Simple_Fighter );
				Unit player2 = playerSquad.SpawnUnit<UnitController>( unitPrefab );
				player2.SpawnWeapon( weaponBank.GetPrefab( WeaponModel.Red_Laser ) );
				player2.SpawnWeapon( weaponBank.GetPrefab( WeaponModel.Blue_Cannon ) );
			}

			/* ADD ENEMY SQUADS */
			for (int i = 0; i < levelProperties.AIStart.Count; i++)
			{				
				Squad aiSquad = new Squad( squads, levelProperties.AIStart[i].position, "AISquad-" + i, SquadFaction.AI_1 );
				squads.Add( aiSquad );

				for (int ii = 0; ii < 1; ii++)
				{
					unitPrefab = unitBank.GetPrefab( UnitModel.Test_Unit );
					Unit ai = aiSquad.SpawnUnit<AIController>( unitPrefab );
					ai.SpawnWeapon( weaponBank.GetPrefab( WeaponModel.Green_Laser ) );
					ai.SpawnWeapon( weaponBank.GetPrefab( WeaponModel.Red_Laser ) );
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
    }
}