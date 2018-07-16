using UnityEngine;

namespace AIRogue.GameObjects {

	/// <summary>
	/// A gameplay unit used in Pawn of Kings.
	/// </summary>
	abstract class Shield : MonoBehaviour
	{
		protected virtual void Awake()
		{

		}

		protected virtual void Start()
		{

		}

		public abstract void SetShieldPercentage(float percent);
	}
}