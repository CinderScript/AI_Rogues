using UnityEngine;

namespace AIRogue.GameObjects {

	/// <summary>
	/// A gameplay unit used in Pawn of Kings.
	/// </summary>
	class Projectile : Damager
	{
		public GameObject shieldImpactPrefab = null;
		public GameObject hullImpactPrefab = null;

		/// <summary>
		/// The Unit that fired this Damage object
		/// </summary>
		public Unit Owner { get; set; }
		public float MaxFlightTime { get; set; }


		protected override void Start()
		{
			Destroy( gameObject, MaxFlightTime );

			Collider[] shipColliders = Owner.GetComponentsInChildren<Collider>();
			Collider projectileCol = GetComponentInChildren<Collider>();
			foreach (var shipCol in shipColliders)
			{
				Physics.IgnoreCollision( projectileCol, shipCol, true );
			}
		}

		void OnCollisionEnter(Collision collision)
		{
			IDamageable damageable = collision.collider.GetComponentInParent<IDamageable>();
			damageable.TakeDamage( Owner, Damage, collision );

			spawnEffect( collision );

			Destroy( gameObject );
		}

		void spawnEffect(Collision collision)
		{
			var tag = collision.collider.gameObject.tag;
			GameObject impactPrefab;
			if (tag == "Shield")
			{
				impactPrefab = shieldImpactPrefab;
			}
			else
			{
				impactPrefab = hullImpactPrefab;
			}

			GameObject effect = Instantiate(
					impactPrefab,
					collision.contacts[0].point,
					Quaternion.LookRotation( collision.contacts[0].normal ) );
			ParticleSystem particles = effect.GetComponent<ParticleSystem>();

			Destroy( effect, particles.main.duration );
		}
	}
}