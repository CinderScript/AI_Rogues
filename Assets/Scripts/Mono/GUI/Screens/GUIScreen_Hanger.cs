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

		[Header( "Hanger Screen - screens" )]
		public GUIScreen_WeaponsMarket WeaponsMarketScreen;

		[Header( "Hanger Screen - data" )]
		public WeaponBank WeaponLibrary;


		//[Header( "Hanger Screen - data" )]
		public Unit Unit_Specs { get;  set; }
		public UnitPersistence Unit_Save { get; set; }

		protected override void Awake()
		{
			base.Awake();
			OnOpened.AddListener( ShowScreen );
			WeaponSlots.OnItemSelected.AddListener( UpdateWeaponInfo );
		}

		private void ShowScreen()
		{
			UpdateWeaponSlots();
			UpdateShipInfo();

			WeaponSlots.Items[0].Toggle.isOn = true;

			ShipIcon.sprite = Unit_Specs.Icon;
			ShipName.text = Unit_Specs.UnitType.ToString();
			ShipValue.text = GetTotalShipValue();
		}

		private void UpdateWeaponSlots()
		{
			for (int i = 0; i < WeaponSlots.Items.Count; i++)
			{
				var item = (ListItem_WeaponSlot)WeaponSlots.Items[i];

				// if ship has slot, then list weapon if found
				if (i < Unit_Specs.WeaponMountCount)
				{
					item.gameObject.SetActive( true );
					var equippedCount = Unit_Save.Weapons.Count;

					if (i < equippedCount)
					{
						var weap = WeaponLibrary.GetWeapon( Unit_Save.Weapons[i] );
						item.Name.text = weap.WeaponName.ToString();
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
			ShipInfo.SetText( Unit_Specs );
		}

		private string GetTotalShipValue()
		{
			float tValue = Unit_Specs.Value;
			return tValue.ToString("$0");
		}
	}
}