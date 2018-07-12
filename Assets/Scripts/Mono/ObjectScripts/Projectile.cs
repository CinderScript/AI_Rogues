using UnityEngine;

namespace AIRogue.GameObjects {

	/// <summary>
	/// A gameplay unit used in Pawn of Kings.
	/// </summary>
	class Projectile : Damage
	{
		public float MaxFlightTime = 1.5f;

		private float maxLifeTimestamp;

		private void Start()
		{
			maxLifeTimestamp = Time.time + MaxFlightTime;
			Destroy( gameObject, MaxFlightTime );
		}
	}
}