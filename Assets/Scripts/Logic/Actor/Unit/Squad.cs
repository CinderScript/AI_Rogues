using System.Collections.Generic;

using AIRogue.Unity.ObjectProperties;

using UnityEngine;


namespace AIRogue.Logic.Actor
{
	class Squad
	{
        public string Name { get; }
        private readonly UnitBank unitBank;
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
		public Squad(UnitBank unitLoader, Vector3 startPosition, string name )
        {
			this.unitBank = unitLoader;
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
        /// Uses the UnitLoader to create a new unit with the properties defined in in the 
        /// properties list given to the UnitLoader.
        /// </summary>
        /// <param name="unitType"></param>
        /// <param name="spawnLocation"></param>
        public void SpawnUnit<T>(UnitType unitType) where T : UnitController, new()
        {
			// get prefab
            GameObject prefab = unitBank.GetUnitPrefab( unitType );

			if (prefab != null )
            {
				// spawn unit
				GameObject unitSpawn = Object.Instantiate( prefab, newUnitPos(), Quaternion.identity );
				unitSpawn.name = Name + " " + unitType + " " + controllers.Count;

				Unit unit = unitSpawn.GetComponent<Unit>();

				T controller = new T();
                controller.AssignUnit( unit );
                controllers.Add( controller );
            }
            else
            {
                Debug.Log( "Unit type not found in loader:  " + unitType );
            }
        }

		/// <summary>
		/// Calculates the position a new unit should be spawned at.
		/// </summary>
		/// <returns></returns>
		private Vector3 newUnitPos()
		{
			Vector3 pos = new Vector3(	startPosition.x + (SPAWN_SPACING * controllers.Count), 
										startPosition.y, 
										startPosition.z );

			return pos;
		}
    }
}