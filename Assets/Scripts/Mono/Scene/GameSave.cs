using System.Collections.Generic;
using AIRogue.GameObjects;
using AIRogue.Persistence;

using UnityEngine;

namespace AIRogue.Scene
{
	class GameSave : MonoBehaviour {

		[Header( "Player Persistance Data" )]
		public PlayerData PlayerData = new PlayerData();

		void Awake()
		{
			PlayerData.LoadDataFromFile();
		}

		void OnApplicationQuit()
		{
			PlayerData.SaveDataToFile();
		}

		public void NewGame()
		{
			PlayerData.Squad = new List<UnitPersistence>();

			var weaps = new List<WeaponName>();
			weaps.Add( WeaponName.RedLaser );
			weaps.Add( WeaponName.GreenLaser );
			var unit = new UnitPersistence( UnitType.SimpleFighter, weaps );
			PlayerData.Squad.Add( unit );

			weaps = new List<WeaponName>();
			weaps.Add( WeaponName.RedLaser );
			unit = new UnitPersistence( UnitType.TestUnit, weaps );
			PlayerData.Squad.Add( unit );

			PlayerData.Funds = 1000;

			PlayerData.SaveDataToFile();
		}
	}
}