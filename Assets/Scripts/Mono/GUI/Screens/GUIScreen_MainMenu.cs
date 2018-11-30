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

		private PreGameGUIController game;

		protected override void Awake()
		{
			base.Awake();

			game = GetComponentInParent<PreGameGUIController>();
			base.OnOpened.AddListener( this.OnOpened );
		}
		protected override void Start()
		{
			base.Start();
		}
		
		public void ResetSavedInfo()
		{
			game.GameSave.NewGame();
			Debug.Log( game.GameSave.ToString() );
			OnOpened();
		}

		private void OnOpened()
		{
			var funds = game.GameSave.Funds;
			var shipCount = game.GameSave.Squad.Count;
			var level = game.SelectedLevel.DisplayName;
			var earnings = 750;

			Funds.text = $"Funds: ${funds}";
			Ships.text = $"My Ship Count: {shipCount}";
			Location.text = $"Selected Level: {level}";
			Earnings.text = $"Earnings on Next Win: ${earnings}";
		}
	}
}