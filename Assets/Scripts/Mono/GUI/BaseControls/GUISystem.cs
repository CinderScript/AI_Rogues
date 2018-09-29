using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace IronGrimoire.Gui
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

		private void Awake()
		{
			ScreenHistory = new Stack<GUIScreen>();
		}
		private void Start()
		{
			StartCoroutine( StartSequence() );
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
		IEnumerator StartSequence()
		{
			screens = GetComponentsInChildren<GUIScreen>( true );

			// trigger each screen (and it's controls) Start()
			foreach (var screen in screens)
			{
				screen.gameObject.SetActive( true );
			}

			// make sure fader is active
			if (Fader)
			{
				Fader.gameObject.SetActive( true );
			}

			// wait a few frames (Start() will run)
			int frameCount = 2;
			while (frameCount > 0)
			{
				frameCount--;
				yield return null;
			}

			// hide each screen after they run their Start
			foreach (var screen in screens)
			{
				screen.gameObject.SetActive( false );
			}

			// open the first screen
			StartScreen.OpenScreen();
			CurrentScreen = StartScreen;

			FadeIn();
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

		public void ExitApplication()
		{
			Application.Quit();
		}
	}
}