using System.Collections.Generic;

using AIRogue.GameObjects;
using AIRogue.Persistence;
using AIRogue.Scene;

using UnityEngine;

namespace IronGrimoire.Gui.Game
{
	class PreGameGUIController : MonoBehaviour
	{
		[Header( "Controller Properties" )]
		public GameSave GameSave;
		public UnitBank ShipLibrary;
		public WeaponBank WeaponLibrary;

		public UnitSave SelectedUnit_Save { get; private set; }
		public Unit SelectedUnit_Stats { get; private set; }

		public void SetSelectedUnit(UnitSave unit)
		{
			SelectedUnit_Save = unit;
			SelectedUnit_Stats = ShipLibrary.GetUnit( unit.UnitModel );
		}

		public List<UnitSave> GetPlayerUnits()
		{
			return GameSave.Squad;
		}
		public List<Unit> GetUnitsForSale()
		{
			return ShipLibrary.GetAllUnits();
		}
		public List<Weapon> GetWeaponsForSale()
		{
			return WeaponLibrary.GetAllWeapons();
		}

		public Unit GetUnitStats(UnitSave unit)
		{
			return ShipLibrary.GetUnit( unit.UnitModel );
		}
		public Weapon GetWeaponStats(WeaponModel weaponID)
		{
			return WeaponLibrary.GetWeapon( weaponID );
		}

		public bool CanAffordEquippable(Weapon weapon)
		{
			if (weapon.Value > GameSave.Funds)
			{
				return false;
			}
			else
			{
				return true;
			}
		}
		public bool CanAffordEquippable(Unit unit)
		{
			if ( unit.Value > GameSave.Funds )
			{
				return false;
			}
			else
			{
				return true;
			}
		}
		public void BuyUnit(Unit unit)
		{
			UnitSave unitSave = new UnitSave( unit );
			unitSave.Weapons.Add( WeaponModel.Red_Laser );

			GameSave.Squad.Add( unitSave );
			GameSave.Funds -= unit.Value;
		}
		public void SellUnit(UnitSave unit)
		{
			GameSave.Funds += GetShipValueTotal( unit );
			GameSave.Squad.Remove( unit );
		}

		public void BuyWeapon(Weapon weapon)
		{
			SelectedUnit_Save.Weapons.Add( weapon.WeaponModel );
			GameSave.Funds -= weapon.Value;
		}
		public void SellWeapon(WeaponModel weapon)
		{
			SelectedUnit_Save.Weapons.Remove( weapon );
			GameSave.Funds += WeaponLibrary.GetWeapon( weapon ).Value;
		}

		public int GetShipValueTotal(UnitSave unit)
		{
			var total = ShipLibrary.GetUnit( unit.UnitModel ).Value;
			foreach (var weap in unit.Weapons)
			{
				total += WeaponLibrary.GetWeapon( weap ).Value;
			}

			return total;
		}
	}
}