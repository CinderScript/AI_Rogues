
using AIRogue.GameObjects;
using AIRogue.Gui;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace IronGrimoire.Gui.Game
{
	class InGameHud : ViewController
	{
		[Header( "Controls" )]
		public ScrollView PlayerShipsScrollview = null;

		[Header( "Transition Screens" )]
		public GUIScreen InGameMenu = null;

		private InGameGUIController game;

		protected override void Awake()
		{
			base.Awake();

			game = GetComponentInParent<InGameGUIController>();
		}
		public override void OnOpened()
		{
			PopulatePlayerShips();

			// Clear selected object so that if button was pressed, the event
			// system's "Submit" trigger (also button "Fire1") does not keep pressing it.
			EventSystem.current.SetSelectedGameObject( null );
		}
		void PopulatePlayerShips()
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