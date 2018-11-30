using AIRogue.Events;

using UnityEngine;

namespace AIRogue.GameObjects
{
	public class ObjectSpinner : MonoBehaviour
	{
		[Header( "Spin Properties" )]
		public Vector3 DegreesPerSec;
		public Space RelativeSpace;

		void Update()
		{
			transform.Rotate( DegreesPerSec * Time.deltaTime, RelativeSpace );
		}
	}
}