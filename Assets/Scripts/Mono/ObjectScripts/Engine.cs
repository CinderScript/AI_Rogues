using UnityEngine;

namespace AIRogue.GameObjects
{

	class Engine : MonoBehaviour
	{
		private ParticleSystem[] particles;

		protected virtual void Awake()
		{
			particles = GetComponentsInChildren<ParticleSystem>();
		}

		public void EngineOn()
		{
			foreach (var sys in particles)
			{
				if (!sys.isPlaying)
				{
					sys.Play();
				}
			}
		}
		public void EngineOff()
		{
			foreach (var sys in particles)
			{
				if (sys.isPlaying)
				{
					sys.Stop();
				}
			}
		}
	}
}