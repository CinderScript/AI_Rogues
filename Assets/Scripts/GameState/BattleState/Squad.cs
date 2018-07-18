using System.Collections.Generic;
using AIRogue.GameObjects;
using AIRogue.Scene;

using UnityEngine;


namespace AIRogue.GameState.Battle
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
		/// <param name="unitBank"></param>
		/// <param name="controllerBlueprint"></param>
		/// <param name="name"></param>
		public Squad(UnitBank unitBank, Vector3 startPosition, string name )
        {
			this.unitBank = unitBank;
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
		public void FixedUpdate()
		{
			foreach (var controller in controllers)
			{
				controller.FixedUpdate();
			}
		}

		/// <summary>
		/// Uses the UnitLoader to create a new unit with the properties defined in in the 
		/// properties list given to the UnitLoader.
		/// </summary>
		/// <param name="unitType"></param>
		/// <param name="spawnLocation"></param>
		public Unit SpawnUnit<T>(UnitType unitType) where T : UnitController, new()
        {
			Unit unit = null;

			// get prefab
			GameObject prefab = unitBank.GetPrefab( unitType );

			if (prefab != null )
            {
				// spawn unit
				GameObject unitSpawn = Object.Instantiate( prefab, newUnitPos(), Quaternion.identity );

				unit = unitSpawn.GetComponent<Unit>();
				unit.SquadPosition = controllers.Count;
				unit.Squad = this;

				unitSpawn.name = unit.ToString();

				T controller = new T();
                controller.AssignUnit( unit );
                controllers.Add( controller );
            }
            else
            {
                Debug.Log( "Unit type not found in loader:  " + unitType );
            }

			return unit;
        }

		public override string ToString()
		{
			return Name;
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