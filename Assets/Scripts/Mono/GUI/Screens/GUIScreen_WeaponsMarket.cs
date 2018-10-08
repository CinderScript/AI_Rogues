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
		public Text AvailableMounts = null;

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
			AvailableMounts.text = AvailableWeaponSlots().ToString();

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

			if (AvailableWeaponSlots() > 0)
			{
				if (game.CanAffordEquippable( weapon ))
				{
					game.BuyWeapon( weapon );
				}
				else
				{
					GUISystem.Dialog.Show( $"Could not afford to buy: {weapon.DisplayName}\n\n" +
						$"Selected weapon cost: { weapon.Value}\n" +
						$"Available funds: {game.GameSave.Funds.ToString( "$0" )}" );
				}
			}
			else
			{
				GUISystem.Dialog.Show( "There are no more weapon mounting positions available.  Sell an equipped weapon to make room for another." );
			}

			UpdateScreen();
		}
		public void Sell()
		{
			var equipped = EquippedScrollview.Selected[0].Tagged as Weapon;
			game.SellWeapon( equipped.WeaponModel );

			UpdateScreen();
		}

		int AvailableWeaponSlots()
		{
			return game.SelectedUnit_Stats.WeaponMountCount - game.SelectedUnit_Save.Weapons.Count;
		}
	}
}