using UnityEngine;

using System.Collections.Generic;
using AIRogue.Logic.Actor;
using AIRogue.Logic.Exceptions;

namespace AIRogue.Unity.ObjectProperties {

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
		private void Start()
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

		/// <summary>
		/// Returns the Unit prefab in the bank matching the given UnitType.  
		/// Returns null if no prefab is found.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public GameObject GetUnitPrefab(UnitType type)
		{
			foreach (var prefab in UnitPrefabs)
			{
				if (prefab.GetComponent<Unit>().UnitType == type)
				{
					return prefab;
				}
			}

			return null;
		}
	}
}