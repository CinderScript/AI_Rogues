﻿namespace AIRogue.Logic.Actor
{
	/// <summary>
	/// The UnitController stores one Unit and applies/updates a behavior for that unit.
	/// 
	/// This is a Base class that is extended by specific controller types, 
	/// i.e. AI and Player controllers.  This base class provides
	/// an update method that updates a UnitBehavior and applies the 
	/// affects of that behavior onto the controller's unit and sends relevant 
	/// Event messages to other objects (such as a DeliverDamage event).
	/// 
	/// The implementing class must contain an Update method that is called during 
	/// Unity's Update cycle (in the ArmyManager's update). The implemented Update should 
	/// call this.updateUnit( getBehaviorChooser() ). The getBehaviorChooser delegate 
	/// needs to be defined in the extending class and should return an IBehavior.  This 
	/// IBehavior also needs to be defined specifically for the controller and picks 
	/// the correct Behavior to run on its unit. For AI units, this will be an AI decision 
	/// behavior.  For player units, this will be a behavior that watches for input and 
	/// returns the appropriate action behavior to this Unit.
	/// 
	/// DEPENDENCY:  UnitController is a Generic Type used by the ArmyManager and is 
	/// also used by the GameStateManager, BattleState.
	/// </summary>
	abstract class UnitController : IUnitController {

		public IUnitController controller { get; set; }

        public Unit Unit { get; set; }

        /// <summary>
        /// Must be included so updateUnit(behaviorGetter) logic can be inherited by sub controllers and 
        /// work universally.
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        protected delegate IUnitBehavior getBehaviorChooser(Unit unit);
        private IUnitBehavior behavior = null;  // behavior to be called every update

		public UnitController()
		{
		}

		/// <summary>
		/// The Initialization must be included because ArmyManager uses a UnitController as a 
		/// Generic Type that is instanced.  Only the default constructor of a generic can be 
		/// instanced ( new () ).
		/// </summary>
		/// <param name="unit"></param>
		/// <param name="id"></param>
		public void Initialize(Unit unit, int id)
        {
            Unit = unit;
            Unit.Id = id;
        }

        /// <summary>
        /// The implementing class must contain an Update that should be called during 
        /// Unity's Update cycle. The implemented Update should call 
        /// this.updateUnit( behaviorPickerGetter() ). The behaviorPickerGetter defined 
        /// in the implemented class should return an behavior that should be run first 
        /// by the Unit on it's turn.  For AI units, this will be an AI descision 
        /// making behavior.  For player controlled units, this behavior will listen for 
        /// player input and return the appropriate behavior to this Unit.
        /// </summary>
        /// <returns></returns>
        public abstract bool Update();

        /// <summary>
        /// Should be called by the Update method of a UnitController implementing class.
        /// The delegate passed to updateUnit needs to be defined by the implementing 
        /// class.  The delegate should be a method that returns an IControllerBehavior that 
        /// contains logic for choosing the next Unit's action.  updateUnit will perform the 
        /// action dictated in the IControllerBehavior returned by behaviorPickerGetter each Update. 
        /// Behaviors return themselfs until they have finished and return null, or they return a new 
        /// Behavior.  When a behavior returns null, all behaviors have finished.
        /// </summary>
        /// <param name="behaviorChooserGetter"></param>
        /// <returns>turnEnd: true when all behaviors have finished, else false</returns>
        protected bool updateUnit(getBehaviorChooser behaviorChooserGetter)
        {
            // if behavior == null, behavior picker has not yet run this turn.
            if ( behavior == null )
            {
                behavior = behaviorChooserGetter(Unit);
            }

            // play selected behavior
            IUnitBehavior bahaviorNextUpdate;
            bool turnEnd = behavior.Perform(out bahaviorNextUpdate);

            behavior = bahaviorNextUpdate;
            return turnEnd;
        }
    }

	interface IUnitController
	{
		Unit Unit { get; set; }

		void Initialize(Unit unit, int id);
		bool Update();
	}
}