using System.Collections.Generic;

using AIRogue.GameObjects;

using UnityEngine;

namespace IronGrimoire.Gui.Game
{
	class GUIScreen_Squad : GUIScreen
	{
		[Header( "Squad Screen Properties" )]
		public ScrollView MySquadScrollview;
		public ScrollView ShipMarketScrollview;

		public List<Unit> ShipsInMarket = null;


		protected override void Start()
		{
			base.Start();

			foreach (var ship in ShipsInMarket)
			{
				ListItem_Ship item = (ListItem_Ship)ShipMarketScrollview.AddTemplatedItem();
				item.Initialize( ship );
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