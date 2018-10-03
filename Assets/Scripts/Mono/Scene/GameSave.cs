using System.Collections.Generic;
using System.IO;

using AIRogue.GameObjects;
using AIRogue.Persistence;

using IronGrimoire.Persistence;

using ProtoBuf;

using UnityEngine;

namespace AIRogue.Scene
{
	[ProtoContract]
	class GameSave : MonoBehaviour {

		public static string SAVE_PATH
		{
			get {
				return Path.Combine( Application.persistentDataPath, FILE_NAME );
			}
		}
		public const string FILE_NAME = "GameSave.ser";

		[ProtoMember( 10 )]
		public List<UnitSave> Squad;

		[ProtoMember( 20 )]
		public int Funds;

		void Awake()
		{
			LoadDataFromFile();
		}
		void OnApplicationQuit()
		{
			SaveDataToFile();
		}

		public GameSave()
		{
			Squad = new List<UnitSave>();
		}
		public void NewGame()
		{
			Squad = new List<UnitSave>();

			var weaps = new List<WeaponModel>();
			weaps.Add( WeaponModel.GreenLaser );
			weaps.Add( WeaponModel.RedLaser );
			var unit = new UnitSave( UnitModel.SimpleFighter, weaps );
			Squad.Add( unit );

			weaps = new List<WeaponModel>();
			weaps.Add( WeaponModel.RedLaser );
			unit = new UnitSave( UnitModel.TestUnit, weaps );
			Squad.Add( unit );

			weaps = new List<WeaponModel>();
			weaps.Add( WeaponModel.GreenLaser );
			weaps.Add( WeaponModel.RedLaser );
			unit = new UnitSave( UnitModel.TestUnit, weaps );
			Squad.Add( unit );

			Funds = 2000;

			SaveDataToFile();
		}

		public void SaveDataToFile()
		{
			ProtoUtility.SaveToFile( SAVE_PATH, this );
		}
		public void LoadDataFromFile()
		{
			PlayerData data = ProtoUtility.LoadFromFile<PlayerData>( SAVE_PATH );
			Squad = data.Squad;
			Funds = data.Funds;
		}

		public override string ToString()
		{
			List<string> s = new List<string>();
			s.Add( $"Player Funds: {Funds}, Ship Count: {Squad.Count}" );

			foreach (var unit in Squad)
			{
				s.Add( $"Ship: {unit.UnitModel}, Weapons: {unit.Weapons.Count}" );
			}

			return string.Join( "\n", s );
		}
	}
}