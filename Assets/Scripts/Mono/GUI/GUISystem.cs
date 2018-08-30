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

		[Header( "Screen Transition Fader" )]
		public Image Fader;
		public float FadeDuration = 1;

		public GUIScreen PreviousScreen { get; private set; }
		public GUIScreen CurrentScreen { get; private set; }

		private GUIScreen[] screens = new GUIScreen[0];

		private void Start()
		{
			screens = GetComponentsInChildren<GUIScreen>(true);

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
					CurrentScreen.Close();
					PreviousScreen = CurrentScreen;
				}

				CurrentScreen = newScreen;
				CurrentScreen.gameObject.SetActive( true );
				CurrentScreen.Open();

				OnSwitchedScreens?.Invoke();
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

		public void GoToPrevious(GUIScreen screen)
		{
			if (PreviousScreen)
			{
				SwitchScreens( PreviousScreen );
			}
		}
	}
}