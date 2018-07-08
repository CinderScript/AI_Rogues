using UnityEngine;

namespace AIRogue.GameObjects
{
	/// <summary>
	/// A gameplay unit used in Pawn of Kings.
	/// </summary>
	class Weapon : MonoBehaviour
	{
		public WeaponType WeaponType = WeaponType.Not_Found;

		public float Damage = 1;
		public float RateOfFire = 1;
		public float Range = 10;
		public float Velocity = 10;

		public GameObject AmmoPrefab;

		public Transform BulletSpawnPoint { get; private set; }

		void Start()
		{
			BulletSpawnPoint = gameObject.GetComponentInChildren<BulletSpawnPoint>().transform;
		}
	}

	enum WeaponType {
		Not_Found, RedCannon, BlueCannon, RedLaser, BlueLaser, GreenLaser, Minigun
	}
}