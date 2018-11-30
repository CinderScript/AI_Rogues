using AIRogue.GameObjects;
using AIRogue.Scene;
using UnityEngine;
using UnityEngine.UI;

namespace IronGrimoire.Gui.Game
{
	class ListItem_Location : SelectableListItem
	{
		public Text Location = null;
		public Text EnemyCount = null;
		public Text EnemyRank = null;
		public Text Payout = null;

		public void SetText(LevelProperties properties)
		{
			//Icon.sprite = properties.Icon;
			Location.text = properties.DisplayName;
			EnemyCount.text = properties.EnemyCount.ToString();
			EnemyRank.text = properties.EnemyRank.ToString();
			Payout.text = properties.Payout.ToString();

			//TaggedObject = weapon;
		}
	}
}