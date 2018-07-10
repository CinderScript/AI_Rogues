﻿using UnityEngine;

namespace AIRogue.GameObjects
{
	/// <summary>
	/// A gameplay unit used in Pawn of Kings.
	/// </summary>
	class BallisticWeapon : Weapon
	{
		[Header( "Stats" )]
		public float Velocity = 10;

		protected override void Awake()
		{
			base.Awake();
		}

		public override void Fire()
		{
			// calculate velocity: unit.ridgidbody + Velocity

			GameObject projectile = Instantiate( AmmoPrefab, damagerSpawnPoint.position, Quaternion.identity );
			projectile.name = $"Projectile from {thisUnit.name}.{WeaponType}";
		}
	}
}