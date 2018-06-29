using UnityEngine;

using System.Collections.Generic;

namespace AIRogue.Unity.GameProperties {

    /// <summary>
    /// Provides a container of Units that can be populated in the Unity editor with UnitProperties.
    /// This container is used by the UnitLoader to find the settings and properties that define  
    /// the different Units.  Unit Properties placed in this container, in the Unity Editor, 
    /// will be the units available during gameplay in that scene.
    /// </summary>
    class LevelProperties : MonoBehaviour {

		public int NumberOfEnemySquads = 1;
		public int LevelDifficulty = 1;
		public Transform PlayerStart = null;
		public Transform AIStart = null;

	}
}