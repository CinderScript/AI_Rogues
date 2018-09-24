
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
		}
	}
}