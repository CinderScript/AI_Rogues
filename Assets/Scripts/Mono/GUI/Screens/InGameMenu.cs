
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

			GUIScreen.OnOpened.AddListener( PauseGame );
			GUIScreen.OnClosed.AddListener( UnPauseGame );
		}
		void PauseGame()
		{
			TimeManager.Instance.SetGameplaySpeed( 0.1f );
		}
		void UnPauseGame()
		{
			TimeManager.Instance.SetGameplaySpeed( 1 );
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

		}
	}
}