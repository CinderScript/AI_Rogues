using AIRogue.GameObjects;
using AIRogue.Gui;
using UnityEngine;
using UnityEngine.UI;

namespace IronGrimoire.Gui.Game
{
	internal class ListItem_InGameShip : SelectableListItem, ITooltipParent
	{
		[Header( "Display Values" )]
		public Image Icon = null;
		public Text Hitpoints = null;
		public RectTransform fader = null;

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
			// if ship is still alive
			if (unit.Health > 0)
			{
				var hull = unit.Health.ToString( "0" );
				var shield = unit.Shield.HitPoints.ToString( "0" );

				var hullMax = 50;
				var shieldMax = unit.Shield.HitPointCapacity.ToString( "0" );

				Icon.sprite = unit.Icon;
				Hitpoints.text = $"Hull:   {hull}/{hullMax}\n" +
								 $"Shield:    {shield}/{shieldMax}";
			}
			else
			{
				Hitpoints.text = "Destroyed";
				fader.gameObject.SetActive( true );
			}

			tooltipUnit = unit;
		}
	}
}