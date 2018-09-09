using System.Collections.Generic;
using AIRogue.Events;
using AIRogue.Exceptions;
using AIRogue.GameState.Battle;
using UnityEngine;

namespace AIRogue.GameObjects {

	/// <summary>
	/// A gameplay unit used in AI Rogue.
	/// </summary>
	class Unit : MonoBehaviour, IDamageable
	{
		[Header( "GUI Display" )]
		public UnitType UnitType = UnitType.Not_Found;
		public Sprite Icon = null;
		public int Value = 1000;
		public int WeaponMountCount = 1;
		public int WeaponMountsUsedCount = 1;

		[Header( "Condition" )]
		public float Health = 1;
		public float ShieldCapacity = 1;

		[Header( "Movement" )]
		public float MaxVelocity = 5;
		public float AccelerationForce = 3;
		public float RotationSpeed = 30;

		[Header( "Attack" )]
		public int WeaponLevel = 1;

		[Header( "Particle Effects" )]
		public GameObject DeathExplosionEffect = null;
		public GameObject ShieldImpactEffect = null;


		public Squad Squad { get; private set; }
		public int SquadPosition { get; private set; }
		public void SetSquad(Squad squad, int pos)
		{
			Squad = squad;
			SquadPosition = pos;
		}

		public List<Weapon> Weapons = new List<Weapon>();
		public WeaponMount[] WeaponMounts { get; private set; }
		public Shield Shield { get; private set; }

		public Weapon WeaponWithLongestRange { get; private set; }
		public Weapon WeaponWithShortestRange { get; private set; }

		public TargetingModule FollowModule { get; private set; }

		public Engine Engine { get; private set; }

		/* * * movement vars * * */
		public Rigidbody Rigidbody { get; private set; }
		private float shipVelocityMaxSqr;
		private const float SIDETHRUST_SCALER = 0.6f;
		private float accelerationForceSideways;
		
		void Awake()
		{
			WeaponMounts = GetComponentsInChildren<WeaponMount>();
			Shield = GetComponentInChildren<Shield>();
			Shield.Initialize( ShieldCapacity );

			Rigidbody = GetComponent<Rigidbody>();
			accelerationForceSideways = AccelerationForce * SIDETHRUST_SCALER;
			shipVelocityMaxSqr = MaxVelocity * MaxVelocity;

			FollowModule = new TargetingModule( transform, Rigidbody, MaxVelocity );
			Engine = GetComponentInChildren<Engine>();
		}

		public delegate void DamageTakenReporter(Unit damagedUnit, Unit attacker);
		public DamageTakenReporter OnDamageTaken;

		public delegate void DestroyedReporter(Unit unit);
		public DestroyedReporter OnUnitDestroyed;

		/// <summary>
		/// Applies a force in the forward vector to the ridgidbody of the ship.
		/// Ship will not exceed it's maxVelocity.
		/// Should be called in FixedUpdate().
		/// </summary>
		public void ForwardThrust()
		{
			Rigidbody.AddRelativeForce( Vector3.forward * AccelerationForce );

			// Velocity Cap
			if (Rigidbody.velocity.sqrMagnitude >= shipVelocityMaxSqr)
			{
				Rigidbody.velocity = Rigidbody.velocity.normalized * MaxVelocity;  // preservs directional motion
			}
		}
		/// <summary>
		/// Applies a force in the horizonal vector to the ridgidbody of the ship.
		/// Ship will not exceed it's maxVelocity.
		/// Should be called in FixedUpdate().
		/// </summary>
		public void SideThrust(float inputDirection)
		{
			Rigidbody.AddRelativeForce( Vector3.right * accelerationForceSideways * inputDirection );

			// Velocity Cap
			if (Rigidbody.velocity.sqrMagnitude >= shipVelocityMaxSqr)
			{
				Rigidbody.velocity = Rigidbody.velocity.normalized * MaxVelocity;  // preservs directional motion
			}
		}
		/// <summary>
		/// Rotates the Ship to face the opposite direction of travel.
		/// Does not activate if ship velocity = 0;
		/// </summary>
		public void ReverseTurn()
		{
			if (Rigidbody.velocity.sqrMagnitude != 0)
			{
				transform.rotation = Quaternion.RotateTowards( transform.rotation,   // rotate from current ship rotation
										 Quaternion.LookRotation( -Rigidbody.velocity ),  // to rotation taken from -velocity
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
			OnDamageTaken?.Invoke( this, attacker );
			TakeDamage( damage );
		}
		/// <summary>
		/// Damages the ship without invoking damage reporter or causeing collision effect.
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

			if (WeaponWithLongestRange == null)
			{
				WeaponWithLongestRange = weapon;
			}
			if (WeaponWithShortestRange == null)
			{
				WeaponWithShortestRange = weapon;
			}

			// set with weapon with longest/shortest range
			foreach (var w in Weapons)
			{
				// find longest
				if ( w.Range > WeaponWithLongestRange.Range)
				{
					WeaponWithLongestRange = w;
				}
				// if range is equal, set with weapon with higher dps
				else if (w.Range == WeaponWithLongestRange.Range)
				{
					if (w.WeaponDPS > WeaponWithLongestRange.WeaponDPS)
					{
						WeaponWithLongestRange = w;
					}
				}

				// find shortest
				if (w.Range < WeaponWithShortestRange.Range)
				{
					WeaponWithShortestRange = w;
				}
				// if range is equal, set with weapon with higher dps
				else if (w.Range == WeaponWithShortestRange.Range)
				{
					if (w.WeaponDPS > WeaponWithShortestRange.WeaponDPS)
					{
						WeaponWithShortestRange = w;
					}
				}
			}
		}
		private void destroyShip()
		{
			GameObject effect = Instantiate(
				DeathExplosionEffect, transform.position, transform.rotation );
				
			ParticleSystem particles = effect.GetComponent<ParticleSystem>();

			Destroy( effect, particles.main.duration );
			Destroy( gameObject );

			//EventManager.Instance.QueueEvent( new UnitDestroyedEvent( this ) );
			OnUnitDestroyed?.Invoke( this );

		}
		private void OnDestroy()
		{
		}

		private void OnDisable()
		{
		}


		public override string ToString()
		{
			return $"{Squad.Name}.{SquadPosition}.{UnitType}";
		}
	}

    public enum UnitType
	{
        Not_Found, TestUnit, SimpleFighter, SpaceFighter
    }
}