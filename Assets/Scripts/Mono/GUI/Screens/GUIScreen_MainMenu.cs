using AIRogue.Scene;

using UnityEngine;
using UnityEngine.UI;

namespace IronGrimoire.Gui.Game
{
	class GUIScreen_MainMenu : GUIScreen
	{
		[Header( "Main Menu Screen Properties - Text" )]
		public Text Funds = null;
		public Text Ships = null;
		public Text Location = null;
		public Text Earnings = null;

		private PreGameMenuController gui;

		protected override void Awake()
		{
			base.Awake();

			gui = GetComponentInParent<PreGameMenuController>();
			OnOpened.AddListener( SetText );
		}

		protected override void Start()
		{
			base.Start();
		}
		
		public void Play()
		{

		}
		public void ResetSavedInfo()
		{
			gui.GameSave.NewGame();
			Debug.Log( gui.GameSave.ToString() );
		}

		private void SetText()
		{
			var funds = gui.GameSave.Funds;
			var shipCount = gui.GameSave.Squad.Count;
			var level = "Sector 7";
			var earnings = 750;

			Funds.text = $"Funds: ${funds}";
			Ships.text = $"My Ship Count: {shipCount}";
			Location.text = $"Selected Level: {level}";
			Earnings.text = $"Earnings on Next Win: ${earnings}";
		}
	}
}