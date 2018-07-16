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
		[Header( "Gameplay Properties" )]
		public UnitType UnitType = UnitType.Not_Found;
		public GameObject DeathExplosionEffect;
		public GameObject ShieldImpactEffect;

		[Header( "Condition" )]
		public float Health = 1;
		public float ShieldCapacity = 1;

		[Header( "Movement" )]
		public float MaxVelocity = 5;
		public float AccelerationForce = 3;
		public float RotationSpeed = 30;

		[Header( "Attack" )]
		public int WeaponLevel = 1;

		/* * * Assigned during gameplay * * */
		public Squad Squad { get; set; }
		public Unit Target { get; set; }
		public int SquadPosition { get; set; }

		public List<Weapon> Weapons = new List<Weapon>();
		public WeaponMount[] WeaponMounts { get; private set; }
		public Shield Shield { get; private set; }

		void Awake()
		{
			WeaponMounts = GetComponentsInChildren<WeaponMount>();
			Shield = GetComponentInChildren<Shield>();
			Shield.Initialize( ShieldCapacity );
		}

		public void SpawnWeapon(GameObject weaponPrefab)
		{
			if (weaponPrefab == null)
			{
				Debug.LogError( $"The SpawnWeapon method on Unit {name} was passed a null value" );
			}

			if (weaponPrefab.GetComponent<Weapon>() == null)
			{
				string msg = $"The Prefab named \"{weaponPrefab.name}\" does not have a Weapon component attached.";
				throw new WeaponComponentNotAttachedException( msg );
			}

			int weaponNumber = Weapons.Count;
			Transform mount = WeaponMounts[weaponNumber].transform;

			// spawn unit
			var weaponSpawn = Instantiate( weaponPrefab, mount.position, Quaternion.identity, mount );
			var weapon = weaponSpawn.GetComponent<Weapon>();
			weapon.WeaponPosition = Weapons.Count;

			Weapons.Add( weapon );
		}
		public void FireWeapons()
		{
			foreach (var weapon in Weapons)
			{
				weapon.FireWeapon();
			}
		}
		public void TakeDamage(float damage)
		{
			var breakThrough = Shield.TakeDamage( damage );
			damageHull( breakThrough );

			if (Health <= 0)
			{
				destroyShip();
			}
			Debug.Log( $"{this} has Shields: {Shield.HitPoints}, Health: {Health}" );
		}

		private void damageHull(float damage)
		{
			Health -= damage;
		}
		private void destroyShip()
		{
			GameObject effect = Instantiate(
				DeathExplosionEffect, transform.position, transform.rotation );
				
			ParticleSystem particles = effect.GetComponent<ParticleSystem>();

			Destroy( effect, particles.main.duration );
			Destroy( gameObject );
		}

		public override string ToString()
		{
			return $"{Squad}.{SquadPosition}.{UnitType}";
		}
	}


    public enum UnitType {
        Not_Found, TestUnit, SimpleFighter, SpaceFighter
    }
}