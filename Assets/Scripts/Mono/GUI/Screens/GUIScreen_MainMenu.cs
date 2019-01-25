using AIRogue.Scene;

using UnityEngine;
using UnityEngine.UI;

namespace IronGrimoire.Gui.Game
{
	class GUIScreen_MainMenu : GUIScreen
	{
		[Header( "Main Menu Screen Properties - controls" )]
		public Text Funds = null;
		public Text Ships = null;
		public Text Location = null;
		public Text Earnings = null;
		public Slider MasterVolume = null;

		private PreGameGUIController game;

		protected override void Awake()
		{
			base.Awake();

			game = GetComponentInParent<PreGameGUIController>();
			base.OnOpened.AddListener( this.OnOpenedHandler );
		}
		protected override void Start()
		{
			base.Start();

			MasterVolume.value = game.GameSave.MasterVolume;
		}
		
		public void ResetSavedInfo()
		{
			game.GameSave.NewGame();
			Debug.Log( game.GameSave.ToString() );
			OnOpenedHandler();
		}
		public void Cheat()
		{
			game.GameSave.Funds += game.SelectedLevel.Payout;

			var funds = game.GameSave.Funds;
			Funds.text = $"Funds: ${funds}";
		}

		private void OnOpenedHandler()
		{
			var funds = game.GameSave.Funds;
			var shipCount = game.GameSave.Squad.Count;
			var level = game.SelectedLevel.DisplayName;
			var earnings = game.SelectedLevel.Payout.ToString("$0");

			Funds.text = $"Funds: ${funds}";
			Ships.text = $"My Ship Count: {shipCount}";
			Location.text = $"Selected Level: {level}";
			Earnings.text = $"Earnings on Next Win: {earnings}";

			game.DisplayFirstLoadMessage();
		}
	}
}