using System.Collections.Generic;

using AIRogue.GameObjects;
using AIRogue.Scene;
using UnityEngine;

namespace IronGrimoire.Gui.Game
{
	class GUIScreen_Squad : GUIScreen
	{
		[Header( "Squad Screen Properties - controls" )]
		public ScrollView MySquadScrollview;
		public ScrollView ShipMarketScrollview;


		[Header( "Squad Screen Properties - data" )]
		public SaveGame SaveGame;
		public UnitBank ShipsInMarket = null;

		protected override void Start()
		{
			base.Start();

			List<Unit> units = ShipsInMarket.GetAllUnits();
			foreach (var unit in units)
			{
				ListItem_Ship item = (ListItem_Ship)ShipMarketScrollview.AddTemplatedItem();
				item.Initialize( unit );
			}
		}
		public void OpenSelectedShipHanger(GUIScreen_Hanger screen)
		{
			Unit selectedUnit = (Unit)MySquadScrollview.SelectedItem_Last.TaggedObject;
			GUISystem.SwitchScreens( screen );
			screen.Setup( selectedUnit );
		}
	}
}