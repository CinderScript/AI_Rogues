using System.Collections.Generic;

using AIRogue.Exceptions;
using AIRogue.GameObjects;

using UnityEngine;

namespace AIRogue.Scene
{
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

		public List<Weapon> GetAllWeapons()
		{
			List<Weapon> weapons = new List<Weapon>();
			var gos = WeaponPrefabs;
			for (int i = 0; i < gos.Count; i++)
			{
				weapons.Add( gos[i].GetComponent<Weapon>() );
			}

			return weapons;
		}

		/// <summary>
		/// Returns the Unit prefab in the bank matching the given UnitType.  
		/// Returns null if no prefab is found.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public GameObject GetPrefab(WeaponModel id)
		{
			foreach (var prefab in WeaponPrefabs)
			{
				if (prefab.GetComponent<Weapon>().WeaponModel == id)
				{
					return prefab;
				}
			}

			var msg = $"The Weapon prefab of type \"{id}\" could not be " +
				"found in the WeaponBank.  A null value was returned";
			Debug.LogWarning( msg );
			return null;
		}
		public Weapon GetWeapon(WeaponModel weapon)
		{
			return GetPrefab( weapon ).GetComponent<Weapon>();
		}
	}
}