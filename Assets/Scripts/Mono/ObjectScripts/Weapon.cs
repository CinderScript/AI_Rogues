using AIRogue.Exceptions;
using UnityEngine;

namespace AIRogue.GameObjects
{
	/// <summary>
	/// A gameplay unit used in Pawn of Kings.
	/// </summary>
	abstract class Weapon : MonoBehaviour
	{
		public WeaponType WeaponType = WeaponType.Not_Found;

		[Header( "Stats" )]
		public float Damage = 1;
		/// <summary>
		/// Shots per second
		/// </summary>
		public float RateOfFire = 1; // shots per sec
		/// <summary>
		/// How far away the damager should reach before it despawns or stops
		/// </summary>
		public float Range = 20;

		[Header( "Projectile, Laser, etc..." )]
		public GameObject DamagerPrefab;  // keep here and not in inheriting class...  
										  // The Damager should be Instantiated in an object pool or as a
										  // single object (particle system) and passed a reference to this Unit
										  // in the Start method

		public int WeaponPosition { get; set; }

		protected Unit thisUnit;
		protected Transform damagerSpawnPoint;
		private float nextShotTimestamp;

		protected virtual void Awake()
		{
			thisUnit = transform.root.GetComponent<Unit>();
			damagerSpawnPoint = GetComponentInChildren<BulletSpawnPoint>().transform;

			if (DamagerPrefab == null)
			{
				Debug.LogError( $"The DamagerPrefab on Weapon {this} was given a null value" );
			}

			if (DamagerPrefab.GetComponent<Damage>() == null)
			{
				string msg = $"The DamagerPrefab on Weapon {this} does not have a Damage component attached";
				throw new DamageComponentNotAttachedException( msg );
			}
		}

		protected virtual void Start()
		{

		}

		public void FireWeapon()
		{
			if ( nextShotTimestamp < Time.time )
			{
				var secondsPerShot = 1 / RateOfFire;
				nextShotTimestamp = Time.time + secondsPerShot;
				activateShot();
			}
		}

		protected abstract void activateShot();

		public override string ToString()
		{
			return $"{thisUnit}.{WeaponPosition}.{WeaponType}";
		}
	}

	enum WeaponType {
		Not_Found, RedCannon, BlueCannon, RedLaser, BlueLaser, GreenLaser, Minigun
	}
}