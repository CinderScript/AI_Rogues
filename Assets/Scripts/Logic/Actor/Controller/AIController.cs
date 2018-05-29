namespace AIRogue.Logic.Actor
{

	/// <summary>
	/// Implements UnitController and used by ArmyManager as a possible Generic type.
	/// This is a controller for AI units and its behaviors will not be choosen 
	/// through the GUI, but through AI logic.
	/// </summary>
	class AIController : UnitController {

        /// <summary>
        /// Updates this controller's unit's behavior.  Returns true when 
        /// this controller is done running all behaviors for this unit's 
        /// turn and is ready to decide the next move. 
        /// 
        /// Called by ArmyManager when updating units.  
        /// </summary>
        /// <returns>True when finished with all behaviors this turn, else false.</returns>
        public override bool Update()
        {
            return updateUnit( aiBehaviorChooser );
        }
        public void FixedUpdate() { }

        protected IUnitBehavior aiBehaviorChooser(Unit unit)
        {
            AIBehaviorChooser action = new AIBehaviorChooser(unit);

            return action;
        }
    }

    /// <summary>
    /// Behavior that calculates the AI's behavior for this unit's turn.
    /// </summary>
    class AIBehaviorChooser : IUnitBehavior {

        private readonly Unit unit;

        public AIBehaviorChooser(Unit unit)
        {
            this.unit = unit;

            // get number of enemy units in range
            // get closest enemy unit
        }
        public bool Perform(out IUnitBehavior nextAction)
        {
            // if no units in range
            nextAction = new Move( unit.Transform );

            return false ;
        }
    }
}