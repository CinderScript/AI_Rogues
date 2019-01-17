using System.Collections;
using System.Collections.Generic;
using IronGrimoire.Audio;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace IronGrimoire.Gui
{
	[RequireComponent( typeof( AudioFader ) )]
	public class GUISystem : MonoBehaviour
	{
		[Header( "GUI System Properties" )]
		public GUIScreen StartScreen;
		public DialogBox Dialog;

		[Header( "GUI System Events" )]
		public UnityEvent OnSwitchedScreens = new UnityEvent();

		[Header( "Scene Transitions" )]
		public Image Fader;
		public float FadeDuration = 1;
		public Text TransitionPercentText;

		[Header( "Background Music" )]
		public float MusicFadeInDuration = 2f;
		public float MusicFadeOutDuration = 1f;

		public Stack<GUIScreen> ScreenHistory { get; private set; }
		public GUIScreen CurrentScreen { get; private set; }

		private GUIScreen[] screens = new GUIScreen[0];
		private AudioSource music;
		private AudioFader musicFader;

		private void Awake()
		{
			ScreenHistory = new Stack<GUIScreen>();
			music = GetComponent<AudioSource>();
			if (music)
			{
				music.playOnAwake = false;
				if (music.isPlaying)
				{
					music.Stop();
				}
			}
			musicFader = GetComponent<AudioFader>();
		}
		private void Start()
		{
			StartCoroutine( StartSequence() );
		}
		IEnumerator StartSequence()
		{
			TransitionPercentText.gameObject.SetActive( false );

			screens = GetComponentsInChildren<GUIScreen>( true );

			// trigger each screen (and it's controls) Start()
			Dialog.gameObject.SetActive( true );
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
			int frameCount = 3;
			while (frameCount > 0)
			{
				frameCount--;
				yield return null;
			}

			// hide each screen after they run their Start
			Dialog.gameObject.SetActive( false );
			foreach (var screen in screens)
			{
				screen.gameObject.SetActive( false );
			}

			// open the first screen
			if (StartScreen)
			{
				StartScreen.OpenScreen();
				CurrentScreen = StartScreen;
			}


			musicFader?.FadeIn(MusicFadeInDuration);
			FadeIn();
		}

		public void SwitchScreens(GUIScreen newScreen)
		{
			ScreenHistory.Push( CurrentScreen );
			OpenScreen( newScreen );
		}
		public void SwitchScreenToPrevious()
		{
			OpenScreen( ScreenHistory.Pop() );
		}
		public void OpenScreen(GUIScreen nextScreen)
		{
			CurrentScreen.CloseScreen();
			CurrentScreen = nextScreen;
			CurrentScreen.OpenScreen();

			OnSwitchedScreens?.Invoke();
		}
		public void ResetHistory()
		{
			ScreenHistory.Clear();
		}

		public void LoadScene(string scene, bool showProgress = true)
		{
			StartCoroutine( LoadSceneAsync( scene, showProgress ) );
		}
		IEnumerator LoadSceneAsync(string scene, bool showProgress)
		{
			FadeOut();

			TransitionPercentText.gameObject.SetActive( true );

			TransitionPercentText.text = "Loading...";

			if (showProgress)
			{
				TransitionPercentText.text = "Loading: 0%";
			}

			// let the user see that we are starting to load (pause)
			yield return new WaitForSecondsRealtime( 0.4f );

			var asyncOperation = SceneManager.LoadSceneAsync( scene );

			musicFader.FadeOut( MusicFadeOutDuration );

			// this value stops the scene from displaying when it's finished loading
			asyncOperation.allowSceneActivation = false;

			while (!asyncOperation.isDone)
			{
				if (showProgress)
				{
					// loading bar progress
					var loadingProgress = Mathf.Clamp01( asyncOperation.progress / 0.9f ) * 100;
					TransitionPercentText.text = $"Loading: {loadingProgress.ToString("###%")}";
				}

				// scene has loaded as much as possible, the last 10% can't be multi-threaded
				if (asyncOperation.progress >= 0.9f)
				{
					if (showProgress)
					{
						//Change the Text to show the Scene is ready
						TransitionPercentText.text = "Loading Finished";

						////Wait to you press the space key to activate the Scene
						//if (Input.anyKey)

						// let the user see that we are done (pause)
						yield return new WaitForSecondsRealtime( 0.75f );
					}

					while (musicFader.IsFading)
					{
						yield return null;
					}

					//Activate the Scene
					asyncOperation.allowSceneActivation = true;
				}

				yield return null;
			}
		}

		public void FadeIn()
		{
			Fader?.CrossFadeAlpha( 0, FadeDuration / 2, true );
		}
		public void FadeOut()
		{
			Fader?.CrossFadeAlpha( 1, FadeDuration / 2, true );
		}

		public void ExitApplication()
		{
			Application.Quit();
		}
	}

	public enum LoadLevelMode
	{
		Simple, Progress_Plus_Any_Key
	} 
}