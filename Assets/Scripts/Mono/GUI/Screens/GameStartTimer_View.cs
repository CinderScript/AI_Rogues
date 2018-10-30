
using AIRogue.GameObjects;
using AIRogue.Gui;

using UnityEngine;
using UnityEngine.UI;

namespace IronGrimoire.Gui.Game
{
	class GameStartTimer_View : ViewController
	{
		[Header( "Controls" )]
		public ScrollView EnemyShipsScrollview = null;

		[Header( "Transition Screens" )]
		public GUIScreen InGameHUD = null;

		private InGameGUIController game;

		public override void UpdateView()
		{
		}

		protected override void Awake()
		{
			base.Awake();

			game = GetComponentInParent<InGameGUIController>();

			GUIScreen.OnOpened.AddListener( PauseGame );
			GUIScreen.OnClosed.AddListener( UnPauseGame );
		}
		void PauseGame()
		{
			TimeManager.Instance.SetGameplaySpeed( 0.1f );
		}
		void UnPauseGame()
		{
			TimeManager.Instance.SetGameplaySpeed( 1 );
		}
	}
}