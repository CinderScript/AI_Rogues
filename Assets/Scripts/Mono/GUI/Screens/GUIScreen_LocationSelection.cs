using System.Collections.Generic;

using AIRogue.GameObjects;

using UnityEngine;

namespace IronGrimoire.Gui.Game
{
	class GUIScreen_LocationSelection : GUIScreen
	{
		[Header( "Location Selection Screen Properties" )]
		public ScrollView LocationsScrollview;

		public List<GameObject> AvailableLocations = null;


		protected override void Start()
		{
			base.Start();

			foreach (var location in AvailableLocations)
			{
				ListItem_Location item = (ListItem_Location)LocationsScrollview.AddTemplatedItem();
				item.Initialize( location );
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