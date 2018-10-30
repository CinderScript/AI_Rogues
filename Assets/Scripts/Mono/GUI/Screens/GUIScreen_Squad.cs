using System.Collections.Generic;

using AIRogue.GameObjects;
using AIRogue.Persistence;
using AIRogue.Scene;
using UnityEngine;
using UnityEngine.UI;

namespace IronGrimoire.Gui.Game
{
	class GUIScreen_Squad : GUIScreen
	{
		[Header( "Squad Screen Properties - controls" )]
		public Text Funds = null;
		public ScrollView MySquadScrollview = null;
		public ScrollView ShipMarketScrollview = null;
		public GUIScreen_Hanger HangerScreen = null;


		private PreGameGUIController game;

		protected override void Awake()
		{
			base.Awake();
			game = GetComponentInParent<PreGameGUIController>();

			// update squad scrollview each time this screen is shown
			OnOpened.AddListener( UpdateScreen );
		}
		protected override void Start()
		{
			base.Start();

			// populate ship market scrollview
			PopulateMarket();
		}
		public void OpenSelectedShipHanger()
		{
			var unit_saved = (UnitSave)MySquadScrollview.SelectedItem_Last.Tagged;
			game.SetSelectedUnit( unit_saved );

			GUISystem.SwitchScreens( HangerScreen );
		}
		public void Buy()
		{
			var unit = ShipMarketScrollview.Selected[0].Tagged as Unit;

			if (game.CanAffordEquippable(unit))
			{
				game.BuyUnit( unit );
				UpdateScreen();
			}
			else
			{
				GUISystem.Dialog.Show( $"Could not afford to buy: {unit.gameObject.name}" );
			}
		}
		public void Sell()
		{
			var unit = MySquadScrollview.Selected[0].Tagged as UnitSave;
			game.SellUnit( unit );

			UpdateScreen();
		}

		void UpdateScreen()
		{
			PopulateSquad();
			Funds.text = game.GameSave.Funds.ToString("Funds: $0");
		}
		void PopulateMarket()
		{
			ShipMarketScrollview.Clear();

			List<Unit> units = game.GetUnitsForSale();
			foreach (var unit in units)
			{
				ListItem_Ship item = (ListItem_Ship)ShipMarketScrollview.AddTemplatedItem();
				item.SetText( unit );
				item.Tagged = unit;
			}
		}
		void PopulateSquad()
		{
			MySquadScrollview.Clear();

			foreach (var unitSave in game.GetPlayerUnits())
			{
				var unit = game.GetUnitStats( unitSave );

				ListItem_Ship item = (ListItem_Ship)MySquadScrollview.AddTemplatedItem();
				item.SetText( unit );
				item.Guns.text = $"{unitSave.Weapons.Count} of {unit.WeaponMountCount}";
				item.Tagged = unitSave;
			}
		}
	}
}