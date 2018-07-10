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
		public float RateOfFire = 1;
		public float Range = 10;

		[Header( "Projectile, Laser, etc..." )]
		public GameObject DamagerPrefab;  // keep here and not in inheriting class...  
										  // The Damager should be Instantiated in an object pool or as a
										  // single object (particle system) and passed a reference to this Unit

		protected Unit thisUnit;
		protected Transform damagerSpawnPoint;
		protected float lastShotTimestamp;


		protected virtual void Awake()
		{
			thisUnit = GetComponentInParent<Unit>();
			damagerSpawnPoint = GetComponentInChildren<BulletSpawnPoint>().transform;
		}

		public abstract void Fire();
	}

	enum WeaponType {
		Not_Found, RedCannon, BlueCannon, RedLaser, BlueLaser, GreenLaser, Minigun
	}
}