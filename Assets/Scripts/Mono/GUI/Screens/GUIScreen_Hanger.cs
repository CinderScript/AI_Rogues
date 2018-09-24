using System.Collections.Generic;

using AIRogue.GameObjects;
using AIRogue.Persistence;
using AIRogue.Scene;
using UnityEngine;
using UnityEngine.UI;

namespace IronGrimoire.Gui.Game
{
	class GUIScreen_Hanger : GUIScreen
	{
		[Header( "Hanger Screen - controls" )]
		public Image ShipIcon;
		public Text ShipName;
		public Text ShipValue;

		public Text Weapon1;
		public Text Weapon2;
		public Text Weapon3;
		public Text Weapon4;

		public ControlGroup_WeaponInfo WeaponInfo;

		public Text Shield;
		public Text Hull;
		public Text Velocity;
		public Text Accel;
		public Text Turn;
		public Text Value;

		[Header( "Hanger Screen - screens" )]
		public GUIScreen_WeaponsMarket WeaponsMarketScreen;

		[Header( "Hanger Screen - data" )]
		public WeaponBank WeaponLibrary;


		//[Header( "Hanger Screen - data" )]
		public UnitPersistence Unit { get; set; }

		protected override void Awake()
		{
			base.Awake();
			OnOpened.AddListener( UpdateScreenText );
		}

		private void UpdateScreenText()
		{
			UpdateWeaponInfo();
		}
		private void UpdateWeaponInfo()
		{
			Weapon weap = WeaponLibrary.GetPrefab( Unit.Weapons[0] ).GetComponent<Weapon>();
			WeaponInfo.SetText( weap );
		}
	}
}