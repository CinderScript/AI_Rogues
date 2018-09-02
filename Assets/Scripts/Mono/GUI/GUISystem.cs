using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace IronGrimoire.GuiBase
{
	public class GUISystem : MonoBehaviour
	{
		[Header( "GUI System Properties" )]
		public GUIScreen StartScreen;

		[Header( "GUI System Events" )]
		public UnityEvent OnSwitchedScreens = new UnityEvent();

		[Header( "Scene Transition Fader" )]
		public Image Fader;
		public float FadeDuration = 1;

		public GUIScreen PreviousScreen { get; private set; }
		public GUIScreen CurrentScreen { get; private set; }

		private GUIScreen[] screens = new GUIScreen[0];

		private void Start()
		{
			screens = GetComponentsInChildren<GUIScreen>(true);

			foreach (var screen in screens)
			{
				screen.gameObject.SetActive( false );
			}

			SwitchScreens( StartScreen );

			if (Fader)
			{
				Fader.gameObject.SetActive( true );
			}
			FadeIn();
		}

		public void SwitchScreens(GUIScreen newScreen)
		{
			if (newScreen)
			{
				if (CurrentScreen)
				{
					CurrentScreen.CloseScreen();
					PreviousScreen = CurrentScreen;
				}

				CurrentScreen = newScreen;
				CurrentScreen.OpenScreen();

				OnSwitchedScreens?.Invoke();
			}
		}

		public void SwitchScreenToPrevious()
		{
			if (PreviousScreen)
			{
				SwitchScreens( PreviousScreen );
			}
		}

		public void LoadScene(int sceneIndex)
		{
			StartCoroutine( WaitToLoadScene( sceneIndex ) );
		}

		IEnumerator WaitToLoadScene(int sceneIndex)
		{
			yield return null;
		}

		public void FadeIn()
		{
			Fader?.CrossFadeAlpha( 0, FadeDuration / 2, false );
		}
		public void FadeOut()
		{
			Fader?.CrossFadeAlpha( 1, FadeDuration / 2, false );

		}
	}
}