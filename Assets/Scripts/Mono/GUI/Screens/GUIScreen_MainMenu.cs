
using System.IO;

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
		public SaveGame SaveGame;

		protected override void Start()
		{
			base.Start();
		}
		
		public void Play()
		{

		}
		public void ResetSavedInfo()
		{
			string savePath = Path.Combine( Application.persistentDataPath, "GameSave.ser" );
			Debug.Log( $"Save Path: {savePath}" );

			//PlayerInfo info = new PlayerInfo() { MyString = "First Test!" };

			//ProtoUtility.SaveToFile( savePath, info );
			PlayerData info = ProtoUtility.LoadFromFile<PlayerData>( savePath );
			Debug.Log( $"PlayerInfo: {info.Funds}" );
		}
	}
}