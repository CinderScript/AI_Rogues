using AIRogue.GameObjects;
using AIRogue.Gui;
using UnityEngine;
using UnityEngine.UI;

namespace IronGrimoire.Gui.Game
{
	internal class ListItem_Ship : SelectableListItem, ITooltipParent
	{
		[Header( "Display Values" )]
		public Image Icon = null;
		public Text Name = null;
		public Text Guns = null;
		public Text Value = null;

		[Header( "Tooltip" )]
		public ControlGroup_ShipInfo TooltipPrefab;

		private Unit tooltipUnit;

		RectTransform ITooltipParent.InstantiatedTooltip
		{
			get {
				var tooltip = Instantiate( TooltipPrefab.gameObject ).GetComponent<RectTransform>();

				var shipInfo = tooltip.GetComponentInChildren<ControlGroup_ShipInfo>();
				shipInfo.SetText( tooltipUnit );

				return tooltip;
			}
		}

		public void SetText(Unit unit)
		{
			Icon.sprite = unit.Icon;
			Name.text = unit.DisplayName;
			Value.text = unit.Value.ToString("$0");

			tooltipUnit = unit;
		}
	}
}