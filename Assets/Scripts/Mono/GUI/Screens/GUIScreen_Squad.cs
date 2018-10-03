using System.Collections.Generic;

using AIRogue.GameObjects;
using AIRogue.Persistence;
using AIRogue.Scene;
using UnityEngine;

namespace IronGrimoire.Gui.Game
{
	class GUIScreen_Squad : GUIScreen
	{
		[Header( "Squad Screen Properties - controls" )]
		public ScrollView MySquadScrollview = null;
		public ScrollView ShipMarketScrollview = null;
		public GUIScreen_Hanger HangerScreen = null;


		private PreGameMenuController gui;

		protected override void Awake()
		{
			base.Awake();
			gui = GetComponentInParent<PreGameMenuController>();

			// update squad scrollview each time this screen is shown
			OnOpened.AddListener( ShowScreen );
		}
		protected override void Start()
		{
			base.Start();

			// populate ship market scrollview
			PopulateMarket();
		}
		public void OpenSelectedShipHanger()
		{
			var unit_saved = (UnitSave)MySquadScrollview.SelectedItem_Last.TaggedObject;
			gui.SetSelectedUnit( unit_saved );

			GUISystem.SwitchScreens( HangerScreen );
		}
		public void Buy()
		{

		}
		public void Sell()
		{

		}

		void ShowScreen()
		{
			// populate Squad Scrollview

			MySquadScrollview.ClearScrollview();

			foreach (var unitSave in gui.GetPlayerShips())
			{
				var unit = gui.GetShipStats( unitSave );

				ListItem_Ship item = (ListItem_Ship)MySquadScrollview.AddTemplatedItem();
				item.Initialize( unit );
				item.Guns.text = $"{unitSave.Weapons.Count} of {unit.WeaponMountCount}";
				item.TaggedObject = unitSave;
			}
		}
		void PopulateMarket()
		{
			ShipMarketScrollview.ClearScrollview();

			List<Unit> units = gui.GetShipsForSale();
			foreach (var unit in units)
			{
				ListItem_Ship item = (ListItem_Ship)ShipMarketScrollview.AddTemplatedItem();
				item.Initialize( unit );
				item.TaggedObject = unit;
			}
		}
	}
}