using System.Collections.Generic;

using AIRogue.Unity.GameProperties;

using UnityEngine;


namespace AIRogue.Logic.Actor
{
	class Squad
	{
        public string Name { get; }
        private readonly UnitLoader unitLoader;
		private readonly List<UnitController> controllers;
		private readonly Vector3 startPosition;

		private const int SPAWN_SPACING = 10;

		/// <summary>
		/// Instances a new Squad containing a List of UnitControllers.  A deep copy of controllerBlueprint is made for each 
		/// UnitController in the list (AIController, PlayerController, etc... )
		/// </summary>
		/// <param name="unitLoader"></param>
		/// <param name="controllerBlueprint"></param>
		/// <param name="name"></param>
		public Squad(UnitLoader unitLoader, Vector3 startPosition, string name )
        {
			this.unitLoader = unitLoader;
			this.startPosition = startPosition;
            this.Name = name;
			controllers = new List<UnitController>();
        }

        public void Update()
        {
			foreach (var controller in controllers)
			{
				controller.Update();
			}
        }

        /// <summary>
        /// Loops through each UnitController and spawns that controller's Unit at it's assigned grid Cell.  
        /// The unit's Unity GameObject are named  The army's Name, + the unit's ID, + the unit's Type.
        /// </summary>
        public void SpawnUnits()
        {
			// foreach controller in controllers
			for ( int i = 0; i < controllers.Count; i++ )
            {
				// spawn unit
				Vector3 pos = new Vector3( startPosition.x + (SPAWN_SPACING * i), startPosition.y, startPosition.z );
				controllers[i].SpawnUnit( Name, pos );
            }
        }

        /// <summary>
        /// Uses the UnitLoader to create a new unit with the properties defined in in the 
        /// properties list given to the UnitLoader.
        /// </summary>
        /// <param name="unitType"></param>
        /// <param name="spawnLocation"></param>
        public void AddUnit<T>(UnitType unitType) where T : UnitController, new()
        {
            int id = controllers.Count;
            Unit unit = unitLoader.LoadUnit( unitType );

            if ( unit != null )
            {
                T controller = new T();

                controller.AssignUnit( unit, id );

                controllers.Add( controller );
            }
            else
            {
                Debug.Log( "Unit type not found in loader:  " + unitType );
            }
        }
    }
}