using System.Collections;
using AIRogue.Events;
using AIRogue.GameObjects;

using UnityEngine;

namespace IronGrimoire.Audio
{

	public class AudioFader : MonoBehaviour
	{
		[Header( "Audio Fader Properties" )]
		public AudioSource AudioSource;

		public float MaxVolume = 1;
		public float MinVolume = 0;

		public bool IsFading { get; private set; }
		public float TimeRemaining { get; private set; }

		private Coroutine fading;

		void Awake()
		{
			IsFading = false;

			if (!AudioSource)
			{
				AudioSource = GetComponent<AudioSource>();
			}

			if (AudioSource)
			{
				MaxVolume = AudioSource.volume;
			}
		}

		public void FadeIn(float duration)
		{
			FadeTo( MaxVolume, duration );
		}
		public void FadeOut(float duration)
		{
			FadeTo( MinVolume, duration );
		}

		/// <summary>
		/// Fades the audio source to the volume level indicated.  
		/// </summary>
		/// <param name="targetVolume"></param>
		/// <param name="duration"></param>
		public void FadeTo(float targetVolume, float duration)
		{
			if (IsFading)
			{
				StopCoroutine( fading );
			}

			fading = StartCoroutine( Fade( targetVolume, duration ) );
		}
		IEnumerator Fade(float targetVolume, float duration)
		{
			IsFading = true;

			if (AudioSource)
			{
				// Start audio if not already playing
				if (!AudioSource.isPlaying)
				{
					AudioSource.Play();
					AudioSource.volume = 0;
				}

				float timer = 0;
				float startVolume = AudioSource.volume;

				while (timer < duration)
				{
					timer += Time.deltaTime;
					TimeRemaining = duration - timer;
					Mathf.Lerp( startVolume, targetVolume, timer / duration );
					float volume = Mathf.Lerp( startVolume, targetVolume, timer / duration );
					AudioSource.volume = volume;

					yield return null;
				}

				AudioSource.volume = targetVolume;
			}

			IsFading = false;
		}

		//IEnumerator FadeIn(float fadeDuration)
		//{
		//	if (source)
		//	{
		//		source.volume = 0;

		//		// don't restart music if already playing
		//		if (!source.isPlaying)
		//		{
		//			source.Play();
		//		}

		//		while (source.volume < MaxVolume)
		//		{
		//			float percent = Time.deltaTime / fadeDuration;
		//			source.volume += MaxVolume * percent;
		//			yield return null;
		//		}

		//		source.volume = MaxVolume;
		//	}
		//}
		//IEnumerator FadeOut()
		//{
		//	var startVolume = source.volume;

		//	if (source)
		//	{
		//		while (source.volume > 0)
		//		{
		//			float percent = Time.deltaTime / MusicFadeOutDuration;
		//			source.volume -= startVolume * percent;
		//			yield return null;
		//		}

		//		source.volume = 0;
		//	}
		//}
	}
}