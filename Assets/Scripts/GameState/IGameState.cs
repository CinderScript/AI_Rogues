namespace AIRogue.GameState
{
	/// <summary>
	/// The IGameState interfaces are the states used in the GameStateManager's state machine.
	/// 
	/// DEPENDENCY:  The GameStateManager is dependant on the IGameState interface.
	/// </summary>
	interface IGameState {

        void FixedUpdate();
        void Update();
    }


    enum GameStates {
        StartMenu, UnitSelection, Battle
    }


    class StartMenuState : IGameState {

        public void FixedUpdate()
        {
            
        }

        public void Update()
        {
            
        }
    }
}