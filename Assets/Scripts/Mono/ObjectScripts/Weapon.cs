using AIRogue.Exceptions;
using UnityEngine;

namespace AIRogue.GameObjects
{
	/// <summary>
	/// A gameplay unit used in Pawn of Kings.
	/// </summary>
	abstract class Weapon : MonoBehaviour
	{
		[Header( "Gameplay Properties" )]
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
		public GameObject DamagerPrefab;

		public int WeaponPosition { get; set; }

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

		private float shotCooldownTimer = -1;
		float secondsPerShot;

		protected virtual void Awake()
		{
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
			shotCooldownTimer -= Time.deltaTime;
		}

		public void FireWeapon()
		{
			if ( shotCooldownTimer < 0)
			{
				shotCooldownTimer = secondsPerShot;
				activateShot();
			}
		}

		protected abstract void activateShot();

		public override string ToString()
		{
			return $"{unit}.{WeaponPosition}.{WeaponType}";
		}
	}

	enum WeaponType {
		Not_Found, RedCannon, BlueCannon, RedLaser, BlueLaser, GreenLaser, Minigun
	}
}