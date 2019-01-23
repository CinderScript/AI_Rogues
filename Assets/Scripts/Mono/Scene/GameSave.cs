using System.Collections.Generic;
using System.IO;

using AIRogue.GameObjects;
using AIRogue.Persistence;

using IronGrimoire.Persistence;

using ProtoBuf;

using UnityEngine;

namespace AIRogue.Scene
{
	[ProtoContract( SkipConstructor = true )]
	class GameSave : MonoBehaviour {

		public static string SAVE_PATH
		{
			get {
				return Path.Combine( Application.persistentDataPath, FILE_NAME );
			}
		}
		public const string FILE_NAME = "GameSave.ser";

		public const float DEFAULT_MASTER_VOLUME = 0.821f;

		[ProtoMember( 10 )]
		public List<UnitSave> Squad;

		[ProtoMember( 20 )]
		public int Funds;

		[ProtoMember( 30 )]
		public float MasterVolume;

		void Awake()
		{
			LoadDataFromFile();
		}
		void OnDestroy()
		{
			SaveDataToFile();
		}

		public void NewGame()
		{
			Squad = new List<UnitSave>();

			var weaps = new List<WeaponModel>();
			weaps.Add( WeaponModel.Red_Laser );
			weaps.Add( WeaponModel.Red_Laser );
			var unit = new UnitSave( UnitModel.Simple_Fighter, weaps );
			Squad.Add( unit );

			//weaps = new List<WeaponModel>();
			//weaps.Add( WeaponModel.Red_Laser );
			//unit = new UnitSave( UnitModel.Test_Unit, weaps );
			//Squad.Add( unit );

			//weaps = new List<WeaponModel>();
			//weaps.Add( WeaponModel.Green_Laser );
			//weaps.Add( WeaponModel.Red_Laser );
			//unit = new UnitSave( UnitModel.Test_Unit, weaps );
			//Squad.Add( unit );

			Funds = 1000;

			SaveDataToFile();
		}

		public void SaveDataToFile()
		{
			ProtoUtility.SaveToFile( SAVE_PATH, this );
		}
		public void LoadDataFromFile()
		{
			GameSave data = ProtoUtility.LoadFromFile<GameSave>( SAVE_PATH );

			// If: no file found
			if (data.Squad == null && data.Funds == 0)
			{
				NewGame();
			}
			// else if: file found, but squad count was 0 and protobuf returned null
			else if (data.Squad == null)
			{
				data.Squad = new List<UnitSave>();
				Funds = data.Funds;
			}
			// else: get protobuf data
			else
			{
				Squad = data.Squad;
				Funds = data.Funds;
			}

			if (data.MasterVolume == 0)
			{
				data.MasterVolume = DEFAULT_MASTER_VOLUME;
			}
			MasterVolume = data.MasterVolume;
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