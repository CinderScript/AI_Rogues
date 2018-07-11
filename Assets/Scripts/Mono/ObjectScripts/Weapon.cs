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
		public float RateOfFire = 1; // shots per sec
		public float Range = 20;

		[Header( "Projectile, Laser, etc..." )]
		public GameObject DamagerPrefab;  // keep here and not in inheriting class...  
										  // The Damager should be Instantiated in an object pool or as a
										  // single object (particle system) and passed a reference to this Unit
										  // in the Start method

		protected Unit thisUnit;
		protected Transform damagerSpawnPoint;
		private float nextShotTimestamp;

		protected virtual void Awake()
		{
			thisUnit = GetComponentInParent<Unit>();
			damagerSpawnPoint = GetComponentInChildren<BulletSpawnPoint>().transform;
		}

		protected virtual void Start()
		{
			//if (DamagerPrefab == null)
			//{
			//	Debug.LogError( $"The SpawnWeapon method on Unit {name} was passed a null value" );
			//}

			//if (weaponPrefab.GetComponent<Weapon>() == null)
			//{
			//	string msg = $"The Prefab named \"{weaponPrefab.name}\" does not have a Weapon component attached.";
			//	throw new WeaponComponentNotAttachedException( msg );
			//}
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
			return base.ToString();
		}
	}

	enum WeaponType {
		Not_Found, RedCannon, BlueCannon, RedLaser, BlueLaser, GreenLaser, Minigun
	}
}