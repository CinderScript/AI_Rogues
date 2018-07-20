using System.Collections.Generic;
using AIRogue.Exceptions;
using AIRogue.GameState.Battle;
using UnityEngine;

namespace AIRogue.GameObjects {

	/// <summary>
	/// A gameplay unit used in AI Rogue.
	/// </summary>
	class Unit : MonoBehaviour, IDamageable
	{
		[Header( "Gameplay Properties" )]
		public UnitType UnitType = UnitType.Not_Found;
		public GameObject DeathExplosionEffect = null;
		public GameObject ShieldImpactEffect = null;

		[Header( "Condition" )]
		public float Health = 1;
		public float ShieldCapacity = 1;

		[Header( "Movement" )]
		public float MaxVelocity = 5;
		public float AccelerationForce = 3;
		public float RotationSpeed = 30;

		[Header( "Attack" )]
		public int WeaponLevel = 1;

		/* * * Assigned when instanced by squad * * */
		private string squadName;
		private int squadPosition;
		public void SetSquadID(string name, int pos)
		{
			squadName = name;
			squadPosition = pos;
		}

		public List<Weapon> Weapons = new List<Weapon>();
		public WeaponMount[] WeaponMounts { get; private set; }
		public Shield Shield { get; private set; }

		/* * * movement vars * * */
		private new Rigidbody rigidbody;
		private float shipVelocityMaxSqr;
		private const float SIDETHRUST_SCALER = 0.6f;
		private float accelerationForceSideways;

		void Awake()
		{
			WeaponMounts = GetComponentsInChildren<WeaponMount>();
			Shield = GetComponentInChildren<Shield>();
			Shield.Initialize( ShieldCapacity );

			rigidbody = GetComponent<Rigidbody>();
			accelerationForceSideways = AccelerationForce * SIDETHRUST_SCALER;
			shipVelocityMaxSqr = MaxVelocity * MaxVelocity;
		}

		public delegate void AttackReporter(Unit attacker, float damage);
		public AttackReporter OnAttacked;

		/// <summary>
		/// Applies a force in the forward vector to the ridgidbody of the ship.
		/// Ship will not exceed it's maxVelocity.
		/// Should be called in FixedUpdate().
		/// </summary>
		public void ForwardThrust()
		{
			rigidbody.AddRelativeForce( Vector3.forward * AccelerationForce );

			// Velocity Cap
			if (rigidbody.velocity.sqrMagnitude >= shipVelocityMaxSqr)
			{
				rigidbody.velocity = rigidbody.velocity.normalized * MaxVelocity;  // preservs directional motion
			}
		}
		/// <summary>
		/// Applies a force in the horizonal vector to the ridgidbody of the ship.
		/// Ship will not exceed it's maxVelocity.
		/// Should be called in FixedUpdate().
		/// </summary>
		public void SideThrust(float inputDirection)
		{
			rigidbody.AddRelativeForce( Vector3.right * accelerationForceSideways * inputDirection );

			// Velocity Cap
			if (rigidbody.velocity.sqrMagnitude >= shipVelocityMaxSqr)
			{
				rigidbody.velocity = rigidbody.velocity.normalized * MaxVelocity;  // preservs directional motion
			}
		}
		/// <summary>
		/// Rotates the Ship to face the opposite direction of travel.
		/// Does not activate if ship velocity = 0;
		/// </summary>
		public void ReverseTurn()
		{
			if (rigidbody.velocity.sqrMagnitude != 0)
			{
				transform.rotation = Quaternion.RotateTowards( transform.rotation,   // rotate from current ship rotation
										 Quaternion.LookRotation( -rigidbody.velocity ),  // to rotation taken from -velocity
										 RotationSpeed * Time.deltaTime );
			}
		}
		/// <summary>
		/// Rotates the ship around the y-axis.
		/// </summary>
		/// <param name="ship">Object to rotate</param>
		/// <param name="rotationVelocity">Velocity</param>
		/// <param name="inputDirection">Positive values rotate right, negative left</param>
		public void Rotate(float inputDirection)
		{
			transform.Rotate( 0, RotationSpeed * Time.deltaTime * inputDirection, 0 );
		}
		public void FireWeapons()
		{
			foreach (var weapon in Weapons)
			{
				weapon.FireWeapon();
			}
		}

		/// <summary>
		/// Called by Damage object on collision
		/// </summary>
		/// <param name="attacker"></param>
		/// <param name="damage"></param>
		/// <param name="collision"></param>
		public void TakeDamage(Unit attacker, float damage, Collision collision)
		{
			OnAttacked?.Invoke( attacker, damage );
			TakeDamage( damage );
		}
		/// <summary>
		/// Used by shield to deliver pasthrough damage
		/// </summary>
		/// <param name="damage"></param>
		public void TakeDamage(float damage)
		{
			Health -= damage;

			if (Health <= 0)
			{
				destroyShip();
			}
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
			return $"{squadName}.{squadPosition}.{UnitType}";
		}
	}


    public enum UnitType {
        Not_Found, TestUnit, SimpleFighter, SpaceFighter
    }
}