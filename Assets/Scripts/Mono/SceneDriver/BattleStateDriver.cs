using UnityEngine;

using AIRogue.Logic.GameState;
using AIRogue.Unity.Values;

namespace AIRogue.Unity.Drivers {

    /// <summary>
    /// Each Driver script is the entry point in the current scene into the Unity framework.  
    /// The driver classes get the current instance of the GameStateManager and run that state 
    /// manager's current state in Unity's Update loop.
    /// 
    /// The BattleStateDriver runs the GameStateManager's LoadBattleState method and Update method.
    /// 
    /// DEPENDENCY:  No classes are depenent on the driver classes.  BattleStateDriver has dependancies 
    /// on GridProperties and UnitBank, which are referenced via the Unity Editor.
    /// 
    /// </summary>
    class BattleStateDriver : MonoBehaviour {

        [Header( "Tuning Variables" )]
        public UnitBank UnitBank;

        private GameStateManager game;

        void Awake()
        {
            game = GameStateManager.Instance();
        }

        void Start()
        {
            game.LoadBattleState( UnitBank );
        }

        void FixedUpdate()
        {
            game.FixedUpdateGame();
        }
        void Update()
        {
            game.UpdateGame();
        }
    }
}