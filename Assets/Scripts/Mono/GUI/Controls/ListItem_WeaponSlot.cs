using AIRogue.GameObjects;

using UnityEngine.UI;

namespace IronGrimoire.Gui.Game
{
	class ListItem_WeaponSlot : SelectableListItem
	{
		public Text Name = null;

		public void Initialize(Weapon weapon)
		{
			Name.text = weapon.WeaponName.ToString();
		}
	}
}