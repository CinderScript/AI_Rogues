using System.Collections.Generic;

using AIRogue.Exceptions;
using AIRogue.GameObjects;

using UnityEngine;

namespace AIRogue.Scene
{
	class LevelPropertiesBank : MonoBehaviour {

        [Header( "Game Object Resource" )]
		public List<GameObject> LevelPrefabs;

		// check to make sure all the prefabs have the correct components attached.
		void Start()
		{
			foreach (var level in LevelPrefabs)
			{
				if (level.GetComponent<LevelProperties>() == null)
				{
					string msg = $"The Prefab named \"{level.name}\" placed in the " +
						"LevelPropertiesBank does not have a LevelProperties component attached.";
					throw new WeaponComponentNotAttachedException(msg);
				}
			}
		}

		public List<LevelProperties> GetAllLevels()
		{
			List<LevelProperties> levels = new List<LevelProperties>();
			for (int i = 0; i < LevelPrefabs.Count; i++)
			{
				levels.Add( LevelPrefabs[i].GetComponent<LevelProperties>() );
			}

			return levels;
		}
	}
}