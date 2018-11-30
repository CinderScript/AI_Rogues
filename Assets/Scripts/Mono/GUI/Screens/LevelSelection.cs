
using AIRogue.Scene;
using UnityEngine;
using UnityEngine.UI;

namespace IronGrimoire.Gui.Game
{
	class LevelSelection : ViewController
	{
		[Header( "Controls" )]
		public ScrollView LevelsScrollview = null;
		public Text SelectedLevel = null;

		private PreGameGUIController game;

		protected override void Awake()
		{
			base.Awake();

			game = GetComponentInParent<PreGameGUIController>();
		}
		public override void OnOpened()
		{
			PopulateEnemyShips();
			SelectedLevel.text = "Selected Level: " + game.SelectedLevel.DisplayName;
		}

		public void ApplySelectedLevel()
		{
			game.SelectedLevel = (LevelProperties)LevelsScrollview.Selected[0].Tagged;
			SelectedLevel.text = "Selected Level: " + game.SelectedLevel.DisplayName;
		}

		void PopulateEnemyShips()
		{
			LevelsScrollview.Clear();

			foreach (var level in game.LevelLibrary.GetAllLevels())
			{
				ListItem_Location item = (ListItem_Location)LevelsScrollview.AddTemplatedItem();
				item.SetText( level );
				item.Tagged = level;
			}
		}
	}
}