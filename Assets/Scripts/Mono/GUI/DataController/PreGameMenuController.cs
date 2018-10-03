using System.Collections.Generic;
using AIRogue.GameObjects;
using AIRogue.Persistence;
using AIRogue.Scene;
using UnityEngine;

namespace IronGrimoire.Gui.Game
{
	class PreGameMenuController : MonoBehaviour
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

		public List<UnitSave> GetPlayerShips()
		{
			return GameSave.Squad;
		}
		public List<Unit> GetShipsForSale()
		{
			return ShipLibrary.GetAllUnits();
		}
		public List<Weapon> GetWeaponsForSale()
		{
			return WeaponLibrary.GetAllWeapons();
		}

		public Unit GetShipStats(UnitSave unit)
		{
			return ShipLibrary.GetUnit( unit.UnitModel );
		}
		public Weapon GetWeaponStats(WeaponModel weaponID)
		{
			return WeaponLibrary.GetWeapon( weaponID );
		}
	}
}
