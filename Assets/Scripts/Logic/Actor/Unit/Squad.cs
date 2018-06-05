using System.Collections.Generic;

using AIRogue.Unity.Values;

using UnityEngine;


namespace AIRogue.Logic.Actor
{
	class Squad<T> where T : UnitController, new()
	{

        public string Name { get; private set; }

		private IUnitController c;

		private List<UnitStencil> units = new List<UnitStencil>();

        private List<UnitController> controllers;
        private T activeController;

        private readonly UnitLoader unitLoader;

        /// <summary>
        /// Instance a new ArmyManager
        /// </summary>
        /// <param name="unitBank"></param>
        /// <param name="grid"></param>
        /// <param name="name"></param>
        public Squad(UnitBank unitBank, string name )
        {
            unitLoader = new UnitLoader( unitBank );
            this.Name = name;


        }

        /// <summary>
        /// Runs the update method of the UnitController for who's turn it is.
        /// </summary>
        /// <returns>True when army is finished with it's turn. i.e. all 
        /// units have had a turn.</returns>
        public bool Update()
        {
            // update T (UnitController)
            bool unitFinished = activeController.Update();

            // next UnitController
            if ( unitFinished )
            {
                activeController = getNextUnit();
            }

            // Army Finished
            bool allUnitsFinished = (activeController == null) ? true : false;

            return allUnitsFinished;
        }

        /// <summary>
        /// Prepares the army for starting a new turn.  The active controller is 
        /// set to the first in the list of unit controllers.
        /// </summary>
        /// <returns>True if the army has at least 1 controller left.</returns>
        public bool StartTurn()
        {
            bool armyHasUnits = true;

            if ( controllers.Count < 1 )
            {
                armyHasUnits = false;
            }
            else
            {
                activeController = controllers[0];
            }

            return armyHasUnits;
        }

        /// <summary>
        /// Loops through each UnitController and spawns that controller's Unit at it's assigned grid Cell.  
        /// The unit's Unity GameObject are named  The army's Name, + the unit's ID, + the unit's Type.
        /// </summary>
        public void InstanceUnits()
        {
            for ( int i = 0; i < controllers.Count; i++ )
            {
                Unit unit = controllers[i].Unit;

                // spawn unit at cell
                unit.Transform = (Transform)Object.Instantiate(
                        unit.Prefab, new Vector3( 528, 108, 122), Quaternion.identity );
                unit.Transform.name = Name + unit.Id + " " + unit.Type;
            }
        }

        /// <summary>
        /// Uses the UnitLoader to create a new unit with the properties defined in in the 
        /// properties list given to the UnitLoader.
        /// </summary>
        /// <param name="unitType"></param>
        /// <param name="spawnLocation"></param>
        public void AddUnit(UnitType unitType)
        {
            int id = controllers.Count;
            Unit unit = unitLoader.LoadUnit( unitType );

            if ( unit != null )
            {
                T controller = new T();

                controller.Initialize( unit, id );

                controllers.Add( controller );
            }
            else
            {
                Debug.Log( "Unit type not found in loader:  " + unitType );
            }
        }

        /// <summary>
        /// Finds the unit that comes after the current active unit.  Returns null 
        /// if the current unit is the last unit in the army.
        /// </summary>
        /// <returns></returns>
        private T getNextUnit()
        {
            T nextController = default(T);

            int index = activeController.Unit.Id;

            // start the loop at the active unit's index
            for ( int i = index + 1; i < controllers.Count; i++ )
            {
                if ( controllers[index].Unit.Condition.IsAlive )
                {
                    nextController = controllers[index];
                    break;
                }
            }

            return nextController;
        }

    }
}