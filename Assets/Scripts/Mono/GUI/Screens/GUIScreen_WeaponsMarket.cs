using System.Collections.Generic;

using AIRogue.GameObjects;

using UnityEngine;

namespace IronGrimoire.Gui.Game
{
	class GUIScreen_WeaponsMarket : GUIScreen
	{
		[Header( "Weapons Market Screen Properties" )]
		public ScrollView EquippedScrollview;
		public ScrollView MarketScrollview;

		public List<Weapon> WeaponsInMarket = null;


		protected override void Start()
		{
			base.Start();

			foreach (var weapon in WeaponsInMarket)
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