using System;
using AIRogue.Exceptions;

using UnityEngine;

namespace AIRogue.GameObjects
{
	abstract class Weapon : Purchasable
	{
		[Header( "Gameplay Properties" )]
		public WeaponModel WeaponModel = WeaponModel.Not_Found;
		public override string DisplayName => WeaponModel.ToString().Replace( '_', ' ' );

		/// <summary>
		/// Weapon Type (projectile, laser) to be used when displaying the item in the GUI.
		/// </summary>
		public abstract string WeaponType { get; }

		[Header( "Stats" )]
		public float Damage = 1;
		/// <summary>
		/// Shots per second
		/// </summary>
		public float RateOfFire = 1;
		/// <summary>
		/// How far away the damager should reach before it despawns or stops
		/// </summary>
		public float Range = 20;

		[Header( "Projectile, Laser, etc..." )]
		public GameObject DamagerPrefab = null;

		private new AudioSource audio;

		public int WeaponPosition { get; set; }

		public float TimeSinceActivation { get; private set; }

		// Used for targeting logic
		public abstract float DamagerVelocity { get; }
		public float WeaponDPS { get; private set; }
		public bool InRangeOf(Vector3 position)
		{
			return Vector3.Distance( damagerSpawnPoint.position, position ) < Range;
		}
		public TargetingModule TargetingModule { get; private set; }

		protected Unit unit;
		protected Transform damagerSpawnPoint;

		float secondsPerShot;

		protected virtual void Awake()
		{
			audio = GetComponent<AudioSource>();

			unit = transform.root.GetComponent<Unit>();
			damagerSpawnPoint = GetComponentInChildren<BulletSpawnPoint>().transform;

			if (DamagerPrefab == null)
			{
				Debug.LogError( $"The DamagerPrefab on Weapon {this} was given a null value" );
			}

			if (DamagerPrefab.GetComponent<Damager>() == null)
			{
				string msg = $"The DamagerPrefab on Weapon {this} does not have a Damage component attached";
				throw new DamageComponentNotAttachedException( msg );
			}

			secondsPerShot = 1 / RateOfFire;
			WeaponDPS = Damage * RateOfFire;
			TargetingModule = new TargetingModule( damagerSpawnPoint, unit.GetComponent<Rigidbody>(), DamagerVelocity );
		}
		protected virtual void Start()
		{

		}
		protected virtual void Update()
		{
			TimeSinceActivation += Time.deltaTime;
		}

		public bool FireWeapon()
		{
			if (TimeSinceActivation > secondsPerShot)
			{
				TimeSinceActivation = 0;
				audio?.Play();

				activateShot();

				return true;
			}
			else
			{
				return false;
			}

			//if ( shotCooldownTimer < 0)
			//{
			//	shotCooldownTimer = secondsPerShot;
			//	audio?.Play();
			//	activateShot();
			//}
		}

		protected abstract void activateShot();

		public override string ToString()
		{
			return $"{unit}.{WeaponPosition}.{WeaponModel}";
		}
	}

	enum WeaponModel
	{
		Not_Found = 0,
		Red_Cannon = 2,
		Blue_Cannon = 3,
		Red_Laser = 4,
		Blue_Laser = 5,
		Green_Laser = 6,
		Minigun = 7
	}
}