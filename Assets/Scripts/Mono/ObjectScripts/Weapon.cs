using UnityEngine;

namespace AIRogue.GameObjects
{
	/// <summary>
	/// A gameplay unit used in Pawn of Kings.
	/// </summary>
	class Weapon : MonoBehaviour
	{
		public WeaponType WeaponType = WeaponType.Not_Found;

		[Header( "Stats" )]
		public float Damage = 1;
		public float RateOfFire = 1;
		public float Range = 10;
		public float Velocity = 10;

		[Header( "Projectile, Laser, etc..." )]
		public GameObject AmmoPrefab;

		private Unit thisUnit;
		private Transform damagerSpawnPoint;
		private float lastShotTimestamp;


		void Awake()
		{
			thisUnit = GetComponentInParent<Unit>();
			damagerSpawnPoint = GetComponentInChildren<BulletSpawnPoint>().transform;
		}

		public void Fire()
		{
			// calculate velocity: unit.ridgidbody + Velocity

			GameObject projectile = Instantiate( AmmoPrefab, damagerSpawnPoint.position, Quaternion.identity );
			projectile.name = $"Projectile from {thisUnit.name}.{WeaponType}";
		}
	}

	enum WeaponType {
		Not_Found, RedCannon, BlueCannon, RedLaser, BlueLaser, GreenLaser, Minigun
	}
}