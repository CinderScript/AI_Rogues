using System.Collections.Generic;
using AIRogue.Events;
using AIRogue.GameObjects;
using AIRogue.Scene;

using UnityEngine;


namespace AIRogue.GameState.Battle
{
	class Squad
	{
		public string Name { get; }
		public SquadFaction Faction { get; }
		public readonly List<UnitController> Controllers;
		public List<Squad> EnemySquads { get; private set; }

		private readonly UnitBank unitBank;
		private readonly Vector3 startPosition;

		private const int SPAWN_SPACING = 10;

		/// <summary>
		/// Instances a new Squad containing a List of UnitControllers.  A deep copy of controllerBlueprint is made for each 
		/// UnitController in the list (AIController, PlayerController, etc... )
		/// </summary>
		/// <param name="unitBank"></param>
		/// <param name="controllerBlueprint"></param>
		/// <param name="name"></param>
		public Squad(UnitBank unitBank, Vector3 startPosition, string name, SquadFaction faction )
        {
			this.unitBank = unitBank;
			this.startPosition = startPosition;
            Name = name;
			Faction = faction;

			Controllers = new List<UnitController>();
			EnemySquads = new List<Squad>();
		}

		public void Update()
        {
			foreach (var controller in Controllers)
			{
				controller.Update();
			}
        }
		public void FixedUpdate()
		{
			foreach (var controller in Controllers)
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
				unit.SetSquad( this, Controllers.Count );

				unitSpawn.name = unit.ToString();

				T controller = new T();
                controller.Initialize( unit, this );
                Controllers.Add( controller );
            }
            else
            {
                Debug.Log( "Unit type not found in loader:  " + unitType );
            }

			return unit;
        }

		/// <summary>
		/// Calculates the position a new unit should be spawned at.
		/// </summary>
		/// <returns></returns>
		private Vector3 newUnitPos()
		{
			Vector3 pos = new Vector3(	startPosition.x + (SPAWN_SPACING * Controllers.Count), 
										startPosition.y, 
										startPosition.z );

			return pos;
		}

		public override string ToString()
		{
			return Name;
		}
    }

	enum SquadFaction
	{
		Player, AI_1, AI_2
	}
}