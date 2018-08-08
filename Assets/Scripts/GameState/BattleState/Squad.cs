using System.Collections.Generic;
using System.Linq;
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
		public Unit LeadUnit;
		public List<Squad> AllSquads { get; private set; }

		public Squad[] EngagedSquads = new Squad[0];
		private HashSet<Squad> engagedSquads = new HashSet<Squad>(
				new General.ReferenceEqualityComparer<Squad>() );

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
		public Squad(List<Squad> allSquads, Vector3 startPosition, string name, SquadFaction faction )
        {
			AllSquads = allSquads;
			this.startPosition = startPosition;
            Name = name;
			Faction = faction;

			Controllers = new List<UnitController>();
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
		public Unit SpawnUnit<T>(GameObject prefab) where T : UnitController, new()
        {
			Unit unit;

			// spawn unit
			GameObject unitSpawn = Object.Instantiate( prefab, newUnitPos(), Quaternion.identity );

			unit = unitSpawn.GetComponent<Unit>();
			unit.SetSquad( this, Controllers.Count );
			unit.OnDamageTaken += memberTookDamage;

			unitSpawn.name = unit.ToString();

			T controller = new T();
            controller.Initialize( unit, this );
            Controllers.Add( controller );

			if (Controllers.Count == 1)
			{
				LeadUnit = controller.Unit;
			}

			return unit;
        }
		public void EngageSquad(Squad squad)
		{
			if (!engagedSquads.Contains( squad ))
			{
				engagedSquads.Add( squad );
				EngagedSquads = engagedSquads.ToArray();

				squad.EngageSquad( this );  // engage my squad back
			}
		}
		
		private void memberTookDamage(Unit squadMember, Unit attacker)
		{
			if (ReferenceEquals( attacker.Squad, this ))
			{
				// damaged by ally...
			}
			else {
				EngageSquad( attacker.Squad );
			}
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