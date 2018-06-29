using System.Collections.Generic;
using AIRogue.Logic.Actor;
using AIRogue.Unity.GameProperties;

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
		private readonly UnitLoader unitLoader;
		private readonly LevelProperties levelProperties;

		/// <summary>
		/// Instances a new BattleState creating a list of Squad objects for player and AI.
		/// </summary>
		/// <param name="unitLoader"></param>
		/// <param name="levelProperties"></param>
        public BattleState(UnitLoader unitLoader, LevelProperties levelProperties)
        {
			/* Populate list of Squads
			 * Initialize each Squad with the correct UnitController
			 * Give each Squad a reference to...
			 */

			this.unitLoader = unitLoader;
			this.levelProperties = levelProperties;
			squads = new List<Squad>();


			/* Add Squads to List
			 * create a new Squad and then add units to that squad.
			 * A UnitController Type is defined as the Generics value when 
			 * a unit is added.  This controller instanced and run for the 
			 * unit specified.
			 */

			Squad playerSquad = new Squad( unitLoader, levelProperties.PlayerStart.position, "Player" );
			playerSquad.AddUnit<PlayerController>( UnitType.TestUnit );

			squads.Add( playerSquad );

			for (int i = 0; i < levelProperties.NumberOfEnemySquads; i++)
			{
				Squad aiSquad = new Squad( unitLoader, levelProperties.PlayerStart.position, "AISquad-" + i );
				aiSquad.AddUnit<AIController>( UnitType.TestUnit );
				aiSquad.AddUnit<AIController>( UnitType.TestUnit );

				squads.Add( aiSquad );
			}


			/////////////////////////////
			addTestUnits(); // Temporary
			/////////////////////////////


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