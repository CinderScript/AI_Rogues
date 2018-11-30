
using AIRogue.GameObjects;
using AIRogue.Gui;

using UnityEngine;
using UnityEngine.UI;

namespace IronGrimoire.Gui.Game
{
	class RoundSummary_View : ViewController
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
		}
		public override void OnOpened()
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
			ShipsLost.text = "Player Ships Lost: " + game.GetPlayerDestroyedCount();
			Level.text = "Location: " + game.LevelProperties.DisplayName;
		}

		public void EndGame()
		{
			game.EndGame();
		}
	}
}