using System;
using System.Collections.Generic;

using AIRogue.Exceptions;
using AIRogue.GameObjects;
using IronGrimoire.Persistence;
using UnityEngine;

namespace AIRogue.Scene
{
	/// <summary>
	/// Provides a container of Units that can be populated in the Unity editor with UnitProperties.
	/// This container is used by the UnitLoader to find the settings and properties that define  
	/// the different Units.  Unit Properties placed in this container, in the Unity Editor, 
	/// will be the units available during gameplay in that scene.
	/// </summary>
	class UnitBank : MonoBehaviour {

        [Header( "Game Object Resource" )]
		public List<GameObject> UnitPrefabs;

		// check to make sure all the prefabs have a Unit component so the UnitSpawner 
		// doesn't have any trouble during its search for the correct UnitType
		void Start()
		{
			foreach (var unit in UnitPrefabs)
			{
				if (unit.GetComponent<Unit>() == null)
				{
					string msg = $"The Prefab named \"{unit.name}\" placed in the " +
						"UnitBank does not have a Unit component attached.";
					throw new UnitComponentNotAttachedException(msg);
				}
			}
		}

		public List<Unit> GetAllUnits()
		{
			List<Unit> units = new List<Unit>();
			var gos = UnitPrefabs;
			for (int i = 0; i < gos.Count; i++)
			{
				units.Add( gos[i].GetComponent<Unit>() );
			}

			return units;
		}

		/// <summary>
		/// Returns the Unit prefab in the bank matching the given UnitType.  
		/// Returns null if no prefab is found.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public GameObject GetPrefab(UnitModel type)
		{
			GameObject unitPrefab = null;
			foreach (var prefab in UnitPrefabs)
			{
				Unit unit = prefab.GetComponent<Unit>();
				if (unit.UnitModel == type)
				{
					unitPrefab = prefab;
					break;
				}
			}

			if (unitPrefab == null)
			{
				Debug.Log( "Unit type not found in loader:  " + type );
			}

			return unitPrefab;
		}
		public Unit GetUnit(UnitModel type)
		{
			return GetPrefab( type ).GetComponent<Unit>();
		}

		/// <summary>
		/// Returns a list of Unit prefabs given the selected rank.
		/// If no Units for the given rank are found, units of lower rank
		/// are searched for and given when found.
		/// </summary>
		/// <param name="rank"></param>
		/// <returns></returns>
		public List<GameObject> GetUnitPrefabsByRank(int rank)
		{
			List<GameObject> prefabs = new List<GameObject>();

			if (rank < 1)
			{
				prefabs = null;
			}
			else
			{
				foreach (var prefab in UnitPrefabs)
				{
					Unit unit = prefab.GetComponent<Unit>();
					if (unit.Rank == rank)
					{
						prefabs.Add( prefab );
					}
				}

				if (prefabs.Count < 1)
				{
					prefabs = GetUnitPrefabsByRank( rank - 1 );
				}
			}

			return prefabs;
		}
	}
}