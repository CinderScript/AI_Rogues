using UnityEngine;

namespace AIRogue.GameObjects {

	/// <summary>
	/// A gameplay unit used in Pawn of Kings.
	/// </summary>
	class Projectile : Damager
	{
		public GameObject ImpactEffectPrefab;

		/// <summary>
		/// The Unit that fired this Damage object
		/// </summary>
		public Unit Owner { get; set; }
		public float MaxFlightTime { get; set; }


		protected override void Start()
		{
			Destroy( gameObject, MaxFlightTime );
			Physics.IgnoreCollision( GetComponentInChildren<Collider>(), Owner.GetComponent<Collider>(), true );
		}

		void OnCollisionEnter(Collision collision)
		{
			Unit unit = collision.gameObject.GetComponent<Unit>();
			unit.TakeDamage( Damage );
			Destroy( gameObject );

			GameObject effect = Instantiate(
								ImpactEffectPrefab, 
								collision.contacts[0].point, 
								Quaternion.LookRotation(collision.contacts[0].normal) );
			ParticleSystem particles = effect.GetComponent<ParticleSystem>();
			Destroy( effect, particles.main.duration );
		}
	}
}