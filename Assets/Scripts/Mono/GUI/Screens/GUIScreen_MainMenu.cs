
using UnityEngine;
using UnityEngine.UI;

namespace IronGrimoire.Gui.Game
{
	class GUIScreen_MainMenu : GUIScreen
	{
		[Header( "Main Menu Screen Properties" )]
		public Text Funds = null;
		public Text Ships = null;
		public Text Location = null;
		public Text Earnings = null;

		protected override void Start()
		{
			base.Start();
		}
		
		public void Play()
		{

		}
		public void ResetSavedInfo()
		{

		}
	}
}