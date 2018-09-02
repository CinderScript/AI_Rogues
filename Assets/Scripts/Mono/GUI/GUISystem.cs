using System;
using System.Collections;
using System.Collections.Generic;
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

		public Stack<GUIScreen> ScreenHistory { get; private set; }
		public GUIScreen CurrentScreen { get; private set; }

		private GUIScreen[] screens = new GUIScreen[0];

		void Awake()
		{
			ScreenHistory = new Stack<GUIScreen>();
		}
		void Start()
		{
			screens = GetComponentsInChildren<GUIScreen>( true );

			foreach (var screen in screens)
			{
				screen.gameObject.SetActive( false );
			}

			StartScreen.OpenScreen();
			CurrentScreen = StartScreen;

			if (Fader)
			{
				Fader.gameObject.SetActive( true );
			}
			FadeIn();
		}

		public void SwitchScreens(GUIScreen newScreen)
		{
			ScreenHistory.Push( CurrentScreen );
			switchScreen( newScreen );
		}
		public void SwitchScreenToPrevious()
		{
			switchScreen( ScreenHistory.Pop() );
		}
		void switchScreen(GUIScreen nextScreen)
		{
			CurrentScreen.CloseScreen();
			CurrentScreen = nextScreen;
			CurrentScreen.OpenScreen();

			OnSwitchedScreens?.Invoke();
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