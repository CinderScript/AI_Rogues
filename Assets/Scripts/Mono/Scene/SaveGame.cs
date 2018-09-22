using System.Collections.Generic;

using AIRogue.Exceptions;
using AIRogue.GameObjects;
using IronGrimoire.Persistence;
using UnityEngine;

namespace AIRogue.Scene
{
	class SaveGame : MonoBehaviour {

		[Header( "Player Persistance Data" )]
		public PlayerData PlayerData = new PlayerData();

		void Awake()
		{
			PlayerData.LoadDataFromFile();
		}
	}
}