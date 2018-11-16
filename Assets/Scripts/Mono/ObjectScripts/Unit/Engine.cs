using IronGrimoire.Audio;
using UnityEngine;

namespace AIRogue.GameObjects
{

	class Engine : MonoBehaviour
	{
		[Header( "Engine looks for attached particle systems and audio" )]

		private ParticleSystem[] particles;
		private AudioFader thrusterAudio;

		private const float FADE_DURATION = 0.08f;
		private bool isOn = false;

		protected virtual void Awake()
		{
			particles = GetComponentsInChildren<ParticleSystem>();
			thrusterAudio = GetComponentInChildren<AudioFader>();

			// turn particles off if currently on
			foreach (var pSystem in particles)
			{
				if (pSystem.isPlaying)
				{
					pSystem.Stop();
				}
			}
		}

		public void EngineOn()
		{
			if (!isOn)
			{
				isOn = true;

				// turn engine particles on if off
				foreach (var pSystem in particles)
				{

						pSystem.Play();
				}

				thrusterAudio.FadeIn( FADE_DURATION );
			}
		}
		public void EngineOff()
		{
			if (isOn)
			{
				isOn = false;

				// turn particles off if currently on
				foreach (var pSystem in particles)
				{
					
						pSystem.Stop();
				}

				thrusterAudio.FadeOut( FADE_DURATION, true );
			}
		}
	}
}