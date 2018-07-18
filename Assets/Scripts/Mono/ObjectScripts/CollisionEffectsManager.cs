using UnityEngine;

namespace AIRogue.GameObjects {

	/// <summary>
	/// A gameplay unit used in Pawn of Kings.
	/// </summary>
	class CollisionEffectsManager : MonoBehaviour
	{
		private GameObject effectsParent;

		protected void Awake()
		{
			effectsParent = new GameObject( "Effects" );
			effectsParent.transform.parent = transform;
			effectsParent.transform.localPosition = Vector3.zero;
		}

		public void SpawnEffect(GameObject effectPrefab, Collision collision)
		{
			GameObject effect = Instantiate(
				effectPrefab,
				collision.contacts[0].point,
				Quaternion.LookRotation( collision.contacts[0].normal ),
				transform );

			ParticleSystem particles = effect.GetComponent<ParticleSystem>();
			Destroy( effect, particles.main.duration );
		}
		public void DestroyEffects()
		{
			foreach (Transform child in transform)
			{
				Destroy( child.gameObject );
			}
		}
	}
}