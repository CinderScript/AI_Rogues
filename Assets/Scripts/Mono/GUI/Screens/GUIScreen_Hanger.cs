
using AIRogue.GameObjects;
using AIRogue.Gui;

using UnityEngine;
using UnityEngine.UI;

namespace IronGrimoire.Gui.Game
{
	class GUIScreen_Hanger : GUIScreen
	{
		[Header( "Hanger Screen - controls" )]
		public Image ShipIcon = null;
		public Text ShipName = null;
		public Text ShipValue = null;

		public ControlGroup_WeaponInfo WeaponInfo = null;
		public ControlGroup_ShipInfo ShipInfo = null;
		public ScrollView WeaponSlots = null;

		private PreGameMenuController game;

		protected override void Awake()
		{
			base.Awake();
			game = GetComponentInParent<PreGameMenuController>();

			OnOpened.AddListener( ShowScreen );
			WeaponSlots.OnItemSelected.AddListener( UpdateWeaponInfo );
		}

		private void ShowScreen()
		{
			UpdateWeaponSlots();
			UpdateShipInfo();

			WeaponSlots.Items[0].Toggle.isOn = true;
			UpdateWeaponInfo();

			var unitStats = game.SelectedUnit_Stats;

			ShipIcon.sprite = unitStats.Icon;
			ShipName.text = unitStats.UnitModel.ToString();
			ShipValue.text = GetTotalShipValue();
		}

		private void UpdateWeaponSlots()
		{
			for (int i = 0; i < WeaponSlots.Items.Count; i++)
			{
				var item = (ListItem_WeaponSlot)WeaponSlots.Items[i];
				var unit_Stats = game.SelectedUnit_Stats;
				var unit_Save = game.SelectedUnit_Save;

				// if ship has slot, then list weapon if found
				if (i < unit_Stats.WeaponMountCount)
				{
					item.gameObject.SetActive( true );
					var equippedCount = unit_Save.Weapons.Count;

					if (i < equippedCount)
					{
						var weap = game.GetWeaponStats( unit_Save.Weapons[i] );

						item.Name.text = weap.WeaponModel.ToString();
						item.Tagged = weap;
					}
					else
					{
						item.Name.text = "empty";
						item.Tagged = null;
					}

				}
				else //no weapon slot
				{
					item.gameObject.SetActive( false );
					item.Name.text = "";
					item.Tagged = null;
				}
			}
		}
		private void UpdateWeaponInfo()
		{
			Weapon weap = (Weapon)WeaponSlots.Selected[0].Tagged;
			WeaponInfo.SetText( weap );
		}
		private void UpdateShipInfo()
		{
			ShipInfo.SetText( game.SelectedUnit_Stats );
		}

		private string GetTotalShipValue()
		{
			float tValue = game.GetShipValueTotal( game.SelectedUnit_Save );
			return tValue.ToString("$0");
		}
	}
}