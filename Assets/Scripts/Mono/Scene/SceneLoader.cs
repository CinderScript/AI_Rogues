using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Mono.Scene
{
	class SceneLoader
	{
		private static SceneLoader instance = null;
		public static SceneLoader Instance
		{
			get {
				if (instance == null)
				{
					instance = new SceneLoader();
				}
				return instance;
			}
		}

	}
}
