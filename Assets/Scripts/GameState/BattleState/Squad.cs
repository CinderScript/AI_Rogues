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
		public readonly List<UnitControllerBase> Controllers;
		public Unit LeadUnit;
		public List<Squad> AllSquads { get; private set; }

		public bool InCombat { get; private set; }
		public Squad[] EngagedSquads = new Squad[0];
		private HashSet<Squad> engagedSquads = new HashSet<Squad>(
				new General.ReferenceEqualityComparer<Squad>() );

		private readonly UnitBank unitBank;
		private readonly Vector3 startPosition;

		private const int SPAWN_SPACING = 18
			;

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

			Controllers = new List<UnitControllerBase>();
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

		public Unit SpawnUnit(GameObject prefab)
		{
			Unit unit;

			// spawn unit
			GameObject unitSpawn = Object.Instantiate( prefab, NewUnitPos( Controllers.Count ), Quaternion.identity );

			// enemy AI should face the other direction
			if (Faction != SquadFaction.Player)
			{
				unitSpawn.transform.Rotate( 0, 180, 0 );
			}

			unit = unitSpawn.GetComponent<Unit>();
			unit.SetSquad( this, Controllers.Count );
			unit.OnDamageTaken += MemberTookDamage;

			unitSpawn.name = unit.ToString();

			UnitController controller = new UnitController();
			controller.Initialize( unit, this );
			Controllers.Add( controller );

			if (Controllers.Count == 1)
			{
				LeadUnit = controller.Unit;
			}

			return unit;
		}
		///// <summary>
		///// Uses the UnitLoader to create a new unit with the properties defined in in the 
		///// properties list given to the UnitLoader.
		///// </summary>
		///// <param name="unitType"></param>
		///// <param name="spawnLocation"></param>
		//public Unit SpawnUnit<T>(GameObject prefab) where T : UnitControllerBase, new()
		//      {
		//	Unit unit;

		//	// spawn unit
		//	GameObject unitSpawn = Object.Instantiate( prefab, newUnitPos(Controllers.Count), Quaternion.identity );

		//	unit = unitSpawn.GetComponent<Unit>();
		//	unit.SetSquad( this, Controllers.Count );
		//	unit.OnDamageTaken += memberTookDamage;

		//	unitSpawn.name = unit.ToString();

		//	T controller = new T();
		//          controller.Initialize( unit, this );
		//          Controllers.Add( controller );

		//	if (Controllers.Count == 1)
		//	{
		//		LeadUnit = controller.Unit;
		//	}

		//	return unit;
		//      }
		public void EngageSquad(Squad squad)
		{
			// if not already engaged, add
			if (!engagedSquads.Contains( squad ))
			{
				engagedSquads.Add( squad );
				EngagedSquads = engagedSquads.ToArray();
				
				// recipricate engagement by other squad
				squad.EngageSquad( this );  // engage my squad back

				// if this is the first squad to engage, trip combat event
				if (engagedSquads.Count == 1)
				{
					InCombat = true;
					EventManager.Instance.QueueEvent( new SquadEngagedEvent() );
				}
			}
		}
		public void UnitDestroyed(UnitControllerBase controller)
		{
			Controllers.Remove( controller );

			if (controller.Unit == LeadUnit)
			{
				// select new leader
				if (Controllers.Count > 0)
				{
					LeadUnit = Controllers[0].Unit;
					EventManager.Instance.QueueEvent( new LeadUnitChangedEvent( LeadUnit ) );

					if (Faction == SquadFaction.Player)
					{
						EventManager.Instance.QueueEvent( new PlayerLeaderChangedEvent( LeadUnit ) );
					}
				}
			}
		}
		
		private void MemberTookDamage(Unit squadMember, Unit attacker)
		{
			EventManager.Instance.QueueEvent( new UnitDamagedEvent( squadMember ) );

			if (ReferenceEquals( attacker.Squad, this ))
			{
				// damaged by ally...
			}
			else {
				// if the attacker is not in this squad, engage
				EngageSquad( attacker.Squad );
			}
		}

		/// <summary>
		/// Calculates the position a new unit should be spawned at.
		/// </summary>
		/// <returns></returns>
		private Vector3 NewUnitPos( int unitNumber )
		{
			var rowCapacity = 1;  //same as row number
			var posInRow = 1;

			for (int i = 0; i < unitNumber; i++)
			{
				posInRow++;
				if (posInRow > rowCapacity)
				{
					rowCapacity++;
					posInRow = 1;
				}
			}

			var rowNumber = rowCapacity;

			return GetPyramidPosition(rowNumber, posInRow, startPosition, SPAWN_SPACING);
		}
		Vector3 GetPyramidPosition(int rowNumber, int posInRow, Vector3 startPos, float spacing)
		{
			var spacesFromMiddleToRowEnd = rowNumber - 1;

			var backwardsOffset = Vector3.back * (spacing * spacesFromMiddleToRowEnd);

			// flip the triangle if this is AI squad
			if (Faction != SquadFaction.Player)
			{
				backwardsOffset = Vector3.forward * (spacing * spacesFromMiddleToRowEnd);
			}

			//       1			1,  0 spaces from center to leftmost
			//    2  ,  3		2,  1 space from center to leftmost
			// 4  ,  5  ,  6	3,  2 spaces from center to leftmost
			// unit every two spaces starting at leftmost: spacesFromLeft = (posInRow - 1) * 2

			var leftMostOffset = Vector3.left * (spacing * spacesFromMiddleToRowEnd);
			var spacesFromLeft = (posInRow - 1) * 2;
			var horizontalOffset = leftMostOffset + Vector3.right * (spacing * spacesFromLeft);

			var offset = horizontalOffset + backwardsOffset;

			//Vector3 pos = new Vector3( startPosition.x + (SPAWN_SPACING * unitNumber),
			//							startPosition.y,
			//							startPosition.z );

			return startPos + offset;
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