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

		public Text Weapon1 = null;
		public Text Weapon2 = null;
		public Text Weapon3 = null;
		public Text Weapon4 = null;

		public ControlGroup_WeaponInfo WeaponInfo;
		public ControlGroup_ShipInfo ShipInfo;

		[Header( "Hanger Screen - screens" )]
		public GUIScreen_WeaponsMarket WeaponsMarketScreen;

		[Header( "Hanger Screen - data" )]
		public WeaponBank WeaponLibrary;


		//[Header( "Hanger Screen - data" )]
		public Unit UnitStats { get;  set; }
		public UnitPersistence Unit_PlayerData { get; set; }

		protected override void Awake()
		{
			base.Awake();
			OnOpened.AddListener( UpdateScreenText );
		}

		private void UpdateScreenText()
		{
			UpdateWeaponInfo();
			UpdateShipInfo();

			ShipIcon.sprite = UnitStats.Icon;
			ShipName.text = UnitStats.UnitType.ToString();
			ShipValue.text = GetTotalShipValue();
		}

		private void UpdateWeaponInfo()
		{
			Weapon weap = WeaponLibrary.GetPrefab( Unit_PlayerData.Weapons[0] ).GetComponent<Weapon>();
			WeaponInfo.SetText( weap );
		}
		private void UpdateShipInfo()
		{
			ShipInfo.SetText( UnitStats );
		}

		private string GetTotalShipValue()
		{
			float tValue = UnitStats.Value;
			return tValue.ToString("$0");
		}
	}
}