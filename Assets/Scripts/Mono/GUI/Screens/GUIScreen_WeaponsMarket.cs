using AIRogue.GameObjects;
using UnityEngine;
using UnityEngine.UI;

namespace IronGrimoire.Gui.Game
{
	class GUIScreen_WeaponsMarket : GUIScreen
	{
		[Header( "Weapons Market - controls" )]
		public Image ShipIcon = null;
		public Text Funds = null;

		public ScrollView EquippedScrollview;
		public ScrollView MarketScrollview;

		private PreGameMenuController game;

		protected override void Awake()
		{
			base.Awake();
			game = GetComponentInParent<PreGameMenuController>();

			OnOpened.AddListener( UpdateScreen );
		}

		protected override void Start()
		{
			base.Start();
			PopulateMarket();
		}
		void UpdateScreen()
		{
			ShipIcon.sprite = game.SelectedUnit_Stats.Icon;
			Funds.text = game.GameSave.Funds.ToString( "Funds: $0" );

			PopulateEquipped();
		}
		void PopulateEquipped()
		{
			EquippedScrollview.ClearScrollview();
			foreach (var weapModel in game.SelectedUnit_Save.Weapons)
			{
				ListItem_Weapon item = (ListItem_Weapon)EquippedScrollview.AddTemplatedItem();
				Weapon weapon = game.GetWeaponStats( weapModel );
				item.SetText( weapon );
				item.Tagged = weapon;
			}
		}
		void PopulateMarket()
		{
			MarketScrollview.ClearScrollview();
			foreach ( var weapon in game.GetWeaponsForSale() )
			{
				ListItem_Weapon item = (ListItem_Weapon)MarketScrollview.AddTemplatedItem();
				item.SetText( weapon );
				item.Tagged = weapon;
			}
		}

		public void Buy()
		{
			var weapon = MarketScrollview.Selected[0].Tagged as Weapon;
			game.BuyWeapon( weapon );

			UpdateScreen();
		}
		public void Sell()
		{
			var equipped = EquippedScrollview.Selected[0].Tagged as Weapon;
			game.SellWeapon( equipped.WeaponModel );

			UpdateScreen();
		}
	}
}