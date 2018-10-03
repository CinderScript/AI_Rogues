using AIRogue.GameObjects;

using UnityEngine.UI;

namespace IronGrimoire.Gui.Game
{
	class ListItem_Weapon : SelectableListItem
	{
		public Image Icon = null;
		public Text Name = null;
		public Text Type = null;
		public Text Value = null;

		public void SetText(Weapon weapon)
		{
			Icon.sprite = weapon.Icon;
			Name.text = weapon.WeaponModel.ToString();
			Type.text = weapon.WeaponType_GUI;
			Value.text = weapon.Value.ToString();
		}
	}
}