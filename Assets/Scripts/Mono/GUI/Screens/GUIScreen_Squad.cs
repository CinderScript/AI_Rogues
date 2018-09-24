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
		public ScrollView MySquadScrollview;
		public ScrollView ShipMarketScrollview;
		public GUIScreen_Hanger HangerScreen;


		[Header( "Squad Screen Properties - data" )]
		public GameSave GameSave;
		public UnitBank ShipLibrary;
		public UnitBank ShipsInMarket;

		protected override void Awake()
		{
			base.Awake();

			// update squad scrollview each this screen is shown
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
			var unit_playerData = (UnitPersistence)MySquadScrollview.SelectedItem_Last.TaggedObject;
			var unit = ShipLibrary.GetUnit( unit_playerData.UnitType );

			HangerScreen.Unit_PlayerData = unit_playerData;
			HangerScreen.UnitStats = unit;
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

			var playerDataUnits = GameSave.Squad;

			foreach (var unitPersistence in playerDataUnits)
			{
				var unit = ShipLibrary.GetUnit( unitPersistence.UnitType );

				ListItem_Ship item = (ListItem_Ship)MySquadScrollview.AddTemplatedItem();
				item.Initialize( unit );
				item.TaggedObject = unitPersistence;
			}
		}
	}
}