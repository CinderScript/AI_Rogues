using System;
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


		private PreGameMenuController gui;

		protected override void Awake()
		{
			base.Awake();
			gui = GetComponentInParent<PreGameMenuController>();

			OnOpened.AddListener( ShowScreen );
		}

		protected override void Start()
		{
			base.Start();
			PopulateMarket();
		}
		void ShowScreen()
		{
			PopulateEquipped();
		}
		void PopulateEquipped()
		{
			EquippedScrollview.ClearScrollview();
			foreach (var weapModel in gui.SelectedUnit_Save.Weapons)
			{
				ListItem_Weapon item = (ListItem_Weapon)EquippedScrollview.AddTemplatedItem();
				item.Initialize( gui.GetWeaponStats( weapModel ) );
			}
		}
		void PopulateMarket()
		{
			MarketScrollview.ClearScrollview();
			foreach ( var weapon in gui.GetWeaponsForSale() )
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