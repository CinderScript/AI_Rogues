using AIRogue.GameObjects;

using UnityEngine.UI;

namespace IronGrimoire.Gui.Game
{
	class ListItem_Ship : SelectableListItem
	{
		public Image Icon = null;
		public Text Name = null;
		public Text Guns = null;
		public Text Value = null;

		public void Initialize(Unit unit)
		{
			Icon.sprite = unit.Icon;
			Name.text = unit.UnitModel.ToString();
			Guns.text = $"{unit.WeaponMountsUsedCount} of {unit.WeaponMountCount}";
			Value.text = unit.Value.ToString();
		}
	}
}