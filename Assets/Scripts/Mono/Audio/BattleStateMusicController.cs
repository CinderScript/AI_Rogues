using System.Collections;
using System.Collections.Generic;
using AIRogue.Events;
using AIRogue.GameObjects;

using UnityEngine;

namespace IronGrimoire.Audio
{

	public class BattleStateMusicController : MonoBehaviour
	{
		[Header( "Audio Tracks" )]
		public AudioFader OutOfCombat;
		public AudioFader InCombat;
		public AudioSource RoundFinish;

		[Header( "Controller Properties" )]
		public float FadeDuration = 1;

		private Coroutine fading;
		private bool inCombat = false;

		void Awake()
		{
			EventManager.Instance.AddListener<BattleStateStartEvent>( PlayOutOfCombat );
			EventManager.Instance.AddListener<SquadEngagedEvent>( PlayInCombat );
			EventManager.Instance.AddListener<MatchFinishedEvent>( OnMatchFinished );
		}

		void PlayOutOfCombat(BattleStateStartEvent gameEvent)
		{
			OutOfCombat.AudioSource.Play();
		}
		void PlayInCombat(SquadEngagedEvent gameEvent)
		{
			if (!inCombat)
			{
				inCombat = true;

				OutOfCombat.FadeOut( FadeDuration );
				InCombat.AudioSource.PlayDelayed( 0.2f );

			}
		}
		void OnMatchFinished(MatchFinishedEvent gameEvent)
		{
			InCombat.AudioSource.Stop();
			OutOfCombat.AudioSource.Stop();

			RoundFinish.Play();

			////if (gameEvent.IsWin)
			//{
			//	Win.PlayDelayed( 1f );
			//}
			//else
			//{
			//	Loss.PlayDelayed( 1f );
			//}
		}

		void OnDestroy()
		{
			EventManager.Instance.RemoveListener<BattleStateStartEvent>( PlayOutOfCombat );
			EventManager.Instance.RemoveListener<SquadEngagedEvent>( PlayInCombat );
			EventManager.Instance.RemoveListener<MatchFinishedEvent>( OnMatchFinished );
		}
	}
}