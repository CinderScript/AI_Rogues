namespace AIRogue.Logic.Actor
{

	/// <summary>
	/// Implements UnitController and used by Squad.  This is a controller for Player units.  
	/// This controller will run listeners for player input in addition to running AI descision making.
	/// </summary>
	class PlayerController : UnitController {

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
            return updateUnit( getAction );
        }
        public void FixedUpdate() { }

        protected IUnitBehavior getAction(Unit unit)
        {
            PlayerBehaviorChooser action = new PlayerBehaviorChooser(unit);

            return action;
        }
    }

    /// <summary>
    /// Behavior that listens for GUI input and runs the assosiated IBehavior.
    /// </summary>
    class PlayerBehaviorChooser : IUnitBehavior {

        private readonly Unit unit;

        public PlayerBehaviorChooser(Unit unit)
        {
            this.unit = unit;

            // get number of enemy units in range
            // get closest enemy unit
        }
        /// <summary>
        /// Performs this behavior.  Called by the UnitController's Update.
        /// </summary>
        /// <param name="nextAction"></param>
        /// <returns></returns>
        public bool Perform(out IUnitBehavior nextAction)
        {
            nextAction = new Move( unit.Transform );

            return false;
        }
    }
}