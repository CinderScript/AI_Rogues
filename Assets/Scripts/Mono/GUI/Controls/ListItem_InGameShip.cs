using AIRogue.Events;
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
		private Unit Unit;

		RectTransform ITooltipParent.InstantiatedTooltip
		{
			get {
				var tooltip = Instantiate( TooltipPrefab.gameObject ).GetComponent<RectTransform>();

				var shipInfo = tooltip.GetComponentInChildren<ControlGroup_ShipInfo>();
				shipInfo.SetText( Unit );

				return tooltip;
			}
		}

		protected override void Awake()
		{
			base.Awake();

			EventManager.Instance.AddListener<UnitDamagedEvent>( AShipTookDamage );

		}
		void AShipTookDamage(UnitDamagedEvent gameEvent)
		{
			// if this ship took damage
			if (ReferenceEquals( Unit, gameEvent.Unit ))
			{
				SetText( Unit );
			}
		}

		public void SetText(Unit unit)
		{
			// if ship is still alive
			if (unit.Hull > 0)
			{
				var hull = unit.Hull.ToString( "0" );
				var shield = unit.Shield.HitPoints.ToString( "0" );

				var hullMax = unit.HullCapacity.ToString( "0" );
				var shieldMax = unit.Shield.HitPointCapacity.ToString( "0" );

				Icon.sprite = unit.Icon;
				Hitpoints.text = $"Shield: {shield}/{shieldMax}\n" +
								 $"    Hull: {hull}/{hullMax}";
								 
			}
			else
			{
				Hitpoints.text = "Destroyed";
				fader.gameObject.SetActive( true );
			}

			Unit = unit;
		}

		void OnDestroy()
		{
			EventManager.Instance.RemoveListener<UnitDamagedEvent>( AShipTookDamage );
		}
	}
}