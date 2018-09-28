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


		[Header( "Squad Screen Properties - data" )]
		public GameSave GameSave;
		public UnitBank ShipLibrary;
		public UnitBank ShipsInMarket;

		protected override void Awake()
		{
			base.Awake();

			// update squad scrollview each time this screen is shown
			OnOpened.AddListener( populateSquad );
		}
		protected override void Start()
		{
			base.Start();

			// populate ship market scrollview
			populateMarket();
		}
		public void OpenSelectedShipHanger()
		{
			var unit_saved = (UnitPersistence)MySquadScrollview.SelectedItem_Last.TaggedObject;
			var unit_specs = ShipLibrary.GetUnit( unit_saved.UnitType );

			HangerScreen.Unit_Save = unit_saved;
			HangerScreen.Unit_Specs = unit_specs;

			GUISystem.SwitchScreens( HangerScreen );
		}

		private void populateMarket()
		{
			ShipMarketScrollview.ClearScrollview();

			List<Unit> units = ShipsInMarket.GetAllUnits();
			foreach (var unit in units)
			{
				ListItem_Ship item = (ListItem_Ship)ShipMarketScrollview.AddTemplatedItem();
				item.Initialize( unit );
				item.TaggedObject = unit;
			}
		}
		private void populateSquad()
		{
			MySquadScrollview.ClearScrollview();

			foreach (var unitPersistence in GameSave.Squad)
			{
				var unit = ShipLibrary.GetUnit( unitPersistence.UnitType );

				ListItem_Ship item = (ListItem_Ship)MySquadScrollview.AddTemplatedItem();
				item.Initialize( unit );
				item.Guns.text = $"{unitPersistence.Weapons.Count} of {unit.WeaponMountCount}";
				item.TaggedObject = unitPersistence;
			}
		}
	}
}