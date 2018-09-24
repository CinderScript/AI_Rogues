
using System.IO;

using AIRogue.Persistence;
using AIRogue.Scene;

using IronGrimoire.Persistence;

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

		[Header( "Main Menu Screen Properties - Data" )]
		public GameSave GameSave;

		protected override void Awake()
		{
			base.Awake();

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
			GameSave.NewGame();
			Debug.Log( GameSave.PlayerData.ToString() );
		}

		private void SetText()
		{
			var funds = GameSave.PlayerData.Funds;
			var shipCount = GameSave.PlayerData.Squad.Count;
			var level = "Sector 7";
			var earnings = 750;

			Funds.text = $"Funds: ${funds}";
			Ships.text = $"My Ship Count: {shipCount}";
			Location.text = $"Selected Level: {level}";
			Earnings.text = $"Earnings on Next Win: ${earnings}";
		}
	}
}