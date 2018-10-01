using System.Collections.Generic;

using AIRogue.GameObjects;
using AIRogue.Scene;
using UnityEngine;

namespace IronGrimoire.Gui.Game
{
	class GUIScreen_WeaponsMarket : GUIScreen
	{
		[Header( "Weapons Market - controls" )]
		public ScrollView EquippedScrollview;
		public ScrollView MarketScrollview;

		[Header( "Weapons Market - data" )]
		public WeaponBank WeaponLibrary;

		protected override void Start()
		{
			base.Start();

			foreach ( var weapon in WeaponLibrary.GetAllWeapons() )
			{
				ListItem_Weapon item = (ListItem_Weapon)MarketScrollview.AddTemplatedItem();
				item.Initialize( weapon );
			}
		}

		public void Buy()
		{

		}
		public void Sell()
		{

		}
	}
}