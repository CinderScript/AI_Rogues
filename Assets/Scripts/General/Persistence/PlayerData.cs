using System.Collections.Generic;
using System.IO;
using AIRogue.GameObjects;
using IronGrimoire.Persistence;
using ProtoBuf;
using UnityEngine;

namespace AIRogue.Persistence
{
	[ProtoContract]
	class PlayerData
	{
		public static string SAVE_PATH
		{
			get {
				return Path.Combine( Application.persistentDataPath, FILE_NAME );
			}
		}
		public const string FILE_NAME = "GameSave.ser";

		[ProtoMember( 10 )]
		public List<UnitPersistence> Squad { get; set; }

		[ProtoMember(20)]
		public int Funds { get; set; }

		public PlayerData()
		{
			Squad = new List<UnitPersistence>();
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
				s.Add( $"Ship: {unit.UnitType}, Weapons: {unit.Weapons.Count}" );
			}
			
			return string.Join( "\n", s );
		}
	}
}