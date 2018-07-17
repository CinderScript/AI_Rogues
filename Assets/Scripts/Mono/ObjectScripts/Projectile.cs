using UnityEngine;

namespace AIRogue.GameObjects {

	/// <summary>
	/// A gameplay unit used in Pawn of Kings.
	/// </summary>
	class Projectile : Damager
	{
		public GameObject shieldImpactPrefab;
		public GameObject hullImpactPrefab;

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
			Unit unit = collision.gameObject.GetComponent<Unit>();
			unit.TakeDamage( Damage );

			var tag = collision.contacts[0].otherCollider.gameObject.tag;
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
								Quaternion.LookRotation(collision.contacts[0].normal) );
			ParticleSystem particles = effect.GetComponent<ParticleSystem>();

			Destroy( effect, particles.main.duration );
			Destroy( gameObject );

		}
	}
}