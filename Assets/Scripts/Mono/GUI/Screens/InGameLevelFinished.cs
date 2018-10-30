
using AIRogue.GameObjects;
using AIRogue.Gui;

using UnityEngine;
using UnityEngine.UI;

namespace IronGrimoire.Gui.Game
{
	class InGameLevelFinished : ViewController
	{
		[Header( "Controls" )]
		public Text WinLoss = null;
		public Text ShipsDestroyed = null;
		public Text ShipsLost = null;
		public Text Level = null;
		public Text Earnings = null;


		private InGameGUIController game;

		protected override void Awake()
		{
			base.Awake();

			game = GetComponentInParent<InGameGUIController>();

			GUIScreen.OnOpened.AddListener( PauseGame );
			GUIScreen.OnClosed.AddListener( UnPauseGame );
		}

		public override void UpdateView()
		{
			if (game.GameProgress == LevelProgress.Win)
			{
				WinLoss.text = "You Win!";
			}
			else
			{
				WinLoss.text = "Match Lost.";
			}
		}
		void PauseGame()
		{
			TimeManager.Instance.SetGameplaySpeed( 0.2f );
		}
		void UnPauseGame()
		{
			TimeManager.Instance.SetGameplaySpeed( 1f );
		}

		public void EndGame()
		{
			game.EndGame();
		}
	}
}