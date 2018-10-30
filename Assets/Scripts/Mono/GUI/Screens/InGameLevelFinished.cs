
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
		public Text Payout = null;


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
				WinLoss.text = "Victory!";
				Payout.text = "Payout: $" + game.LevelProperties.Payout;
			}
			else
			{
				WinLoss.text = "Match Lost";
				Payout.text = "Payout: $0";
			}

			ShipsDestroyed.text = "Enemy Ships Destroyed: " + game.GetEnemyDestroyedCount();
			ShipsLost.text = "Enemy Ships Destroyed: " + game.GetPlayerDestroyedCount();
			Level.text = "Location: " + game.LevelProperties.LevelName;
		}
		void PauseGame()
		{
			TimeManager.Instance.SetGameplaySpeed( 0.1f );
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