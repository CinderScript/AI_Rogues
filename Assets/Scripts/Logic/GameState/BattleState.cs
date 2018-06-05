using System.Collections.Generic;
using AIRogue.Logic.Actor;

namespace AIRogue.Logic.GameState
{

	/// <summary>
	/// Inherits IGameState.  The state that controls the gameplay during a battle.
	/// 
	/// The BattleState contains two ArmyManagers, one for the AI's team, and one for the Player's team.
	/// This state keeps track of the current turn and gives control the the correct Army's turn update.
	/// 
	/// DEPENDENCY:  GameStateManager has a dependancy for BattleState.
	/// BattleState has a dependancy for ArmyManager, AIController, and PlayerController.
	/// 
	/// Temp dependncy on Grid for character spawning.  Will be handled by spawner or event system 
	/// in future.
	/// </summary>
	class BattleState : IGameState {

		private readonly List<Squad> squads;

        public BattleState(Squad<AIController> aiArmy, Squad<PlayerController> playerArmy)
        {
			squads = new List<Squad>();

            addTestUnits(); // Temporary

			foreach (var squad in squads)
			{
				squad.InstanceUnits();
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

        /// <summary>
        /// Temporary method for adding Units for testing.  In future, Units will be 
        /// added based on GUI selection.
        /// </summary>
        private void addTestUnits()
        {
        }
    }
}