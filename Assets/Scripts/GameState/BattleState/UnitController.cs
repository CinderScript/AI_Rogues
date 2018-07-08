
using AIRogue.GameObjects;
using AIRogue.GameState.Battle.Behavior;

namespace AIRogue.GameState.Battle
{
	/// <summary>
	/// The UnitController stores one Unit and applies/updates a behavior for that unit.
	/// </summary>
	abstract class UnitController {

		public Unit Unit { get; private set; }

		protected UnitActionController actionController { get; set; }
		protected IUnitBehavior behavior { get; set; }

		public UnitController() { }

		/// <summary>
		/// The Initialization must be included because Squad uses a UnitController as a 
		/// Generic Type that is instanced.  Only the default constructor of a generic can be 
		/// instanced ( new () ).
		/// </summary>
		/// <param name="unit"></param>
		/// <param name="id"></param>
		public virtual void AssignUnit(Unit unit)
        {
            Unit = unit;
			actionController = new UnitActionController( Unit );
        }

        public virtual void Update()
		{
			behavior.Perform();
		}
    }
}