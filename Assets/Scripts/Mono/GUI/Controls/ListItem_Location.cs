using AIRogue.GameObjects;

using UnityEngine.UI;

namespace IronGrimoire.Gui.Game
{
	class ListItem_Location : SelectableListItem
	{
		public Image Icon = null;
		public Text Location = null;
		public Text Enemies = null;
		public Text Winnings = null;

		public void Initialize(Weapon weapon)
		{
			//Icon.sprite = weapon.Icon;
			//Name.text = weapon.WeaponName.ToString();
			//Type.text = weapon.WeaponType_GUI;
			//Value.text = weapon.Value.ToString();

			//TaggedObject = weapon;
		}
	}
}