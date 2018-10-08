using AIRogue.GameObjects;
using AIRogue.Gui;
using UnityEngine;
using UnityEngine.UI;

namespace IronGrimoire.Gui.Game
{
	class ListItem_Weapon : SelectableListItem, ITooltipParent
	{
		[Header( "Display Values" )]
		public Image Icon = null;
		public Text Name = null;
		public Text Type = null;
		public Text Value = null;

		[Header( "Tooltip" )]
		public ControlGroup_WeaponInfo TooltipPrefab;
		private Weapon tooltipData;

		public RectTransform InstantiatedTooltip
		{
			get {
				var tooltip = Instantiate( TooltipPrefab.gameObject ).GetComponent<RectTransform>();

				var weaponInfo = tooltip.GetComponentInChildren<ControlGroup_WeaponInfo>();
				weaponInfo.SetText( tooltipData );

				return tooltip;
			}
		}

		public void SetText(Weapon weapon)
		{
			Icon.sprite = weapon.Icon;
			Name.text = weapon.WeaponModel.ToString();
			Type.text = weapon.WeaponType_GUI;
			Value.text = weapon.Value.ToString();

			tooltipData = weapon;
		}
	}
}