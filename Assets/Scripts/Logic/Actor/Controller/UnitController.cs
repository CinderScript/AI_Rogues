using UnityEngine;

namespace AIRogue.Logic.Actor
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
		public void AssignUnit(Unit unit, int id)
        {
            Unit = unit;
            Unit.Id = id;
        }

		public virtual void SpawnUnit(string squadName, Vector3 position)
		{
			Unit.GameObject = Object.Instantiate( Unit.Prefab, position, Quaternion.identity );
			Unit.GameObject.name = squadName + Unit.Id + " " + Unit.Type;

			actionController = new UnitActionController( Unit );
		}

        public virtual void Update()
		{
			behavior.Perform();
		}
    }
}