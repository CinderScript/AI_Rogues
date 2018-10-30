using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace IronGrimoire.Gui
{
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
			int frameCount = 2;
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

		public void LoadScene(string scene)
		{
			StartCoroutine( LoadSceneAsync( scene ) );
		}
		IEnumerator LoadSceneAsync(string scene)
		{
			FadeOut();

			TransitionPercentText.text = "Loading: 0%";
			TransitionPercentText.gameObject.SetActive( true );

			yield return new WaitForSecondsRealtime( 0.3f );

			var asyncOperation = SceneManager.LoadSceneAsync( scene );

			// this value stops the scene from displaying when it's finished loading
			asyncOperation.allowSceneActivation = false;

			while (!asyncOperation.isDone)
			{
				// loading bar progress
				var loadingProgress = Mathf.Clamp01( asyncOperation.progress / 0.9f ) * 100;
				TransitionPercentText.text = $"Loading: {loadingProgress}%";

				// scene has loaded as much as possible, the last 10% can't be multi-threaded
				if (asyncOperation.progress >= 0.9f)
				{
					//Change the Text to show the Scene is ready
					TransitionPercentText.text = "Press the space bar to continue...";
					//Wait to you press the space key to activate the Scene
					if (Input.GetKeyDown( KeyCode.Space ))
						//Activate the Scene
						asyncOperation.allowSceneActivation = true;
				}

				yield return null;
			}
		}
		IEnumerator wait()
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