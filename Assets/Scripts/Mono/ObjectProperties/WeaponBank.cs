using UnityEngine;

using System.Collections.Generic;
using AIRogue.Logic.Actor;
using AIRogue.Logic.Exceptions;

namespace AIRogue.Unity.ObjectProperties {


    class WeaponBank : MonoBehaviour {

        [Header( "Game Object Resource" )]
		public List<GameObject> WeaponPrefabs;


		// check to make sure all the prefabs have the correct components attached.
		void Start()
		{
			foreach (var weapon in WeaponPrefabs)
			{
				if (weapon.GetComponent<Weapon>() == null)
				{
					string msg = $"The Prefab named \"{weapon.name}\" placed in the " +
						"WeaponBank does not have a Weapon component attached.";
					throw new WeaponComponentNotAttachedException(msg);
				}
			}
		}

		/// <summary>
		/// Returns the Unit prefab in the bank matching the given UnitType.  
		/// Returns null if no prefab is found.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public GameObject GetPrefab(WeaponType type)
		{
			foreach (var prefab in WeaponPrefabs)
			{
				if (prefab.GetComponent<Weapon>().WeaponType == type)
				{
					return prefab;
				}
			}

			return null;
		}
	}
}