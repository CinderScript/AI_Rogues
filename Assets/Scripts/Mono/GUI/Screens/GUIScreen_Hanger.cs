using System;
using System.Collections.Generic;

using AIRogue.GameObjects;
using AIRogue.Gui;
using AIRogue.Persistence;
using AIRogue.Scene;
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

		private PreGameMenuController gui;

		protected override void Awake()
		{
			base.Awake();
			gui = GetComponentInParent<PreGameMenuController>();

			OnOpened.AddListener( ShowScreen );
			WeaponSlots.OnItemSelected.AddListener( UpdateWeaponInfo );
		}

		private void ShowScreen()
		{
			UpdateWeaponSlots();
			UpdateShipInfo();

			WeaponSlots.Items[0].Toggle.isOn = true;

			var unitStats = gui.SelectedUnit_Stats;

			ShipIcon.sprite = unitStats.Icon;
			ShipName.text = unitStats.UnitModel.ToString();
			ShipValue.text = GetTotalShipValue();
		}

		private void UpdateWeaponSlots()
		{
			for (int i = 0; i < WeaponSlots.Items.Count; i++)
			{
				var item = (ListItem_WeaponSlot)WeaponSlots.Items[i];
				var unit_Stats = gui.SelectedUnit_Stats;
				var unit_Save = gui.SelectedUnit_Save;

				// if ship has slot, then list weapon if found
				if (i < unit_Stats.WeaponMountCount)
				{
					item.gameObject.SetActive( true );
					var equippedCount = unit_Save.Weapons.Count;

					if (i < equippedCount)
					{
						var weap = gui.GetWeaponStats( unit_Save.Weapons[i] );

						item.Name.text = weap.WeaponModel.ToString();
						item.TaggedObject = weap;
					}
					else
					{
						item.Name.text = "empty";
						item.TaggedObject = null;
					}

				}
				else //no weapon slot
				{
					item.gameObject.SetActive( false );
					item.Name.text = "";
					item.TaggedObject = null;
				}
			}
		}
		private void UpdateWeaponInfo()
		{
			Weapon weap = (Weapon)WeaponSlots.SelectedItems[0].TaggedObject;
			WeaponInfo.SetText( weap );
		}
		private void UpdateShipInfo()
		{
			ShipInfo.SetText( gui.SelectedUnit_Stats );
		}

		private string GetTotalShipValue()
		{
			float tValue = gui.SelectedUnit_Stats.Value;
			return tValue.ToString("$0");
		}
	}
}