
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

		private InGameGUIController game;

		protected override void Awake()
		{
			base.Awake();

			game = GetComponentInParent<InGameGUIController>();
		}

		public override void UpdateView()
		{
			PopulateEnemyShips();
		}
		void PopulateEnemyShips()
		{
			EnemyShipsScrollview.Clear();

			foreach (var enemyUnit in game.EnemyUnits)
			{
				ListItem_InGameShip item = (ListItem_InGameShip)EnemyShipsScrollview.AddTemplatedItem();
				item.SetText( enemyUnit );
				item.Tagged = enemyUnit;
			}
		}

		public void EndGame()
		{
			game.EndGame();
		}
	}
}