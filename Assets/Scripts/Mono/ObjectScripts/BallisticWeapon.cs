using AIRogue.Exceptions;
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

			if (DamagerPrefab == null)
			{
				Debug.LogError( $"The DamagerPrefab on Weapon {this} was given a null value" );
			}

			if (DamagerPrefab.GetComponent<Projectile>() == null)
			{
				string msg = $"The DamagerPrefab on Weapon {this} does not have a Projectile component attached";
				throw new DamageComponentNotAttachedException( msg );
			}


			// spawn bullet pool
			// set Damage properties like "MaxLifetime", "ThisUnit"
		}

		protected override void Start()
		{
			base.Start();

			// spawn bullet pool
			// set Damage properties like "MaxLifetime", "ThisUnit"
		}

		protected override void activateShot()
		{
			GameObject projectile = Instantiate( DamagerPrefab, damagerSpawnPoint.position, thisUnit.transform.rotation );
			projectile.name = $"Projectile from {thisUnit.name}.{WeaponType}";

			// set velocity
			projectile.GetComponent<Rigidbody>().velocity = Velocity * thisUnit.transform.forward
				+ thisUnit.GetComponent<Rigidbody>().velocity;

			// calculate timestamp for removal
			Projectile bullet = DamagerPrefab.GetComponent<Projectile>();
		}
	}
}