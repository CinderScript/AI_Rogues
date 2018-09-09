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
		/// <summary>
		/// Velocity in units/sec of the projectile
		/// </summary>
		public float Velocity = 10;

		public override float DamagerVelocity
		{
			get {
				return Velocity;
			}
		}

		public override string WeaponType_GUI
		{
			get {
				return "Projectile";
			}
		}

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
			GameObject projectile = Instantiate( DamagerPrefab, damagerSpawnPoint.position, unit.transform.rotation );
			projectile.name = $"Projectile from {unit.name}.{WeaponName}";

			// set velocity
			projectile.GetComponent<Rigidbody>().velocity = Velocity * unit.transform.forward
				+ unit.GetComponent<Rigidbody>().velocity;

			// Initialize Bullet Properties
			Projectile bullet = projectile.GetComponent<Projectile>();
			float flightTime = Range / Velocity;
			bullet.MaxFlightTime = flightTime;
			bullet.Owner = unit;
			bullet.Damage = Damage;
		}
	}
}