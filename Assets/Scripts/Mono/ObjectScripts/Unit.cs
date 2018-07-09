using System.Collections.Generic;
using AIRogue.Exceptions;
using AIRogue.GameState.Battle;
using UnityEngine;

namespace AIRogue.GameObjects {

	/// <summary>
	/// A gameplay unit used in AI Rogue.
	/// </summary>
	class Unit : MonoBehaviour
	{
		[Header( "Unit Values" )]
		public UnitType UnitType = UnitType.Not_Found;

		[Header( "Condition" )]
		public float Health = 1;
		public float Shields = 1;

		[Header( "Movement" )]
		public float MaxVelocity = 5;
		public float AccelerationForce = 3;
		public float RotationSpeed = 30;

		[Header( "Attack" )]
		public int WeaponLevel = 1;
		public List<Weapon> Weapons = new List<Weapon>();

		/* * * Assigned during gameplay * * */
		public Squad Squad { get; set; }
		public Unit Target { get; set; }

		public WeaponMount[] WeaponMounts { get; private set; }

		void Awake()
		{
			WeaponMounts = gameObject.GetComponentsInChildren<WeaponMount>();
		}

		public void SpawnWeapon(GameObject weaponPrefab)
		{
			if (weaponPrefab.GetComponent<Weapon>() == null)
			{
				string msg = $"The Prefab named \"{weaponPrefab.name}\" does not have a Weapon component attached.";
				throw new WeaponComponentNotAttachedException( msg );
			}

			int weaponNumber = Weapons.Count;
			Transform mount = WeaponMounts[weaponNumber].transform;

			// spawn unit
			GameObject weaponSpawn = Instantiate( weaponPrefab, mount.position, Quaternion.identity, mount );
			Weapons.Add( weaponSpawn.GetComponent<Weapon>() );
		}

		public void FireWeapons()
		{
			foreach (var weapon in Weapons)
			{
				weapon.Fire();
			}
		}
	}


    public enum UnitType {
        Not_Found, TestUnit, SimpleFighter, SpaceFighter
    }
}