
using AIRogue.GameObjects;
using AIRogue.Gui;

using UnityEngine;
using UnityEngine.UI;

namespace IronGrimoire.Gui.Game
{
	class InGameHud : ViewController
	{
		[Header( "Controls" )]
		public ScrollView PlayerShipsScrollview = null;

		[Header( "Transition Screens" )]
		public GUIScreen InGameMenu = null;

		private InGameMenuController game;

		protected override void Awake()
		{
			base.Awake();

			game = GetComponentInParent<InGameMenuController>();
		}
		public override void UpdateView()
		{
			PopulateEnemyShips();
		}
		void PopulateEnemyShips()
		{
			PlayerShipsScrollview.Clear();

			foreach (var enemyUnit in game.PlayerUnits)
			{
				ListItem_InGameShip item = (ListItem_InGameShip)PlayerShipsScrollview.AddTemplatedItem();
				item.SetText( enemyUnit );
				item.Tagged = enemyUnit;
			}
		}

		public void OpenMenu()
		{
			GUISystem.SwitchScreens( InGameMenu );
		}
	}
}