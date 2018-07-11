using UnityEngine;

namespace AIRogue.GameObjects
{
	/// <summary>
	/// A gameplay unit used in Pawn of Kings.
	/// </summary>
	class BallisticWeapon : Weapon
	{
		[Header( "Ballistic Weapon Stats" )]
		public float Velocity = 10;

		protected override void Awake()
		{
			base.Awake();



			// spawn bullet pool
				// set Damage properties like "MaxLifetime", "ThisUnit"
		}

		protected override void activateShot()
		{
			GameObject projectile = Instantiate( DamagerPrefab, damagerSpawnPoint.position, Quaternion.identity );
			projectile.name = $"Projectile from {thisUnit.name}.{WeaponType}";

			// set velocity
			projectile.GetComponent<Rigidbody>().velocity = Velocity * thisUnit.transform.forward
				+ thisUnit.GetComponent<Rigidbody>().velocity;
		}
	}
}