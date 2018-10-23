
using AIRogue.GameObjects;
using AIRogue.Gui;

using UnityEngine;
using UnityEngine.UI;

namespace IronGrimoire.Gui.Game
{
	class InGameMenu : ViewController
	{
		[Header( "Controls" )]
		public ScrollView EnemyShipsScrollview = null;

		[Header( "Transition Screens" )]
		public GUIScreen InGameHUD = null;

		private InGameMenuController game;

		protected override void Awake()
		{
			base.Awake();
		}

		public override void UpdateView()
		{
			
		}

		void PopulateEnemyShips()
		{
			EnemyShipsScrollview.Clear();
		}

		void Update()
		{

		}
	}
}