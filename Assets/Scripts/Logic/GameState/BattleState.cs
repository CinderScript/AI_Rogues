﻿using System.Collections.Generic;
using AIRogue.Logic.Actor;
using AIRogue.Unity.ObjectProperties;

namespace AIRogue.Logic.GameState
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
        public BattleState(UnitBank unitBank, LevelProperties levelProperties)
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
			Squad playerSquad = new Squad( unitBank, levelProperties.PlayerStart.position, "Player" );
			playerSquad.SpawnUnit<PlayerController>( UnitType.SimpleFighter );
			playerSquad.SpawnUnit<AIController>( UnitType.TestUnit );

			squads.Add( playerSquad );


			/* ADD ENEMY SQUADS */
			for (int i = 0; i < levelProperties.NumberOfEnemySquads; i++)
			{				
				Squad aiSquad = new Squad( unitBank, levelProperties.AIStart[i].position, "AISquad-" + i );
				aiSquad.SpawnUnit<AIController>( UnitType.TestUnit );
				aiSquad.SpawnUnit<AIController>( UnitType.TestUnit );

				squads.Add( aiSquad );
			}
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
            // NOT IMPLEMENTED
        }
    }
}