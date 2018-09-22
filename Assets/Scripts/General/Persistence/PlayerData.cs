using System.Collections.Generic;
using System.IO;
using ProtoBuf;
using UnityEngine;

namespace IronGrimoire.Persistence
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
		public List<UnitPersistenceData> Squad { get; private set; }

		[ProtoMember(20)]
		public int Funds { get; set; }

		public PlayerData()
		{
			Squad = new List<UnitPersistenceData>();
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
	}
}