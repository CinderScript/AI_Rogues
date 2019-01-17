using AIRogue.Events;

using UnityEngine;

namespace AIRogue.GameObjects
{
	public class UnitCamera : MonoBehaviour
	{
		[Header( "Follow Target" )]
		public GameObject Target = null;

		[Header( "Offsets" )]
		public int HeightOffest;
		public int LengthOffest;

		[Header( "Target Selection" )]
		public float LerpDuration = 0.5f;

		private bool isLerping = false;
		private float lerpTimer = 0;
		private bool hasMatchStarted = false;

		void Awake()
		{
			EventManager.Instance.AddListener<UnitSelectedEvent>( OnUnitSelected );
			EventManager.Instance.AddListenerOnce<MatchStartEvent>( MatchStartEventHandler );
		}

		// Use this for initialization
		void Start()
		{

		}

		// Update is called once per frame
		void LateUpdate()
		{
			if (Target)
			{
				Vector3 targetOffset = new Vector3(
					Target.transform.position.x,
					Target.transform.position.y + HeightOffest,
					Target.transform.position.z - LengthOffest );

				if (isLerping)
				{
					isLerping = LerpToTarget(targetOffset);
				}
				else
				{
					transform.position = targetOffset;
				}
			}
		}

		// Game Event Listener
		void OnUnitSelected(UnitSelectedEvent e)
		{
			if (hasMatchStarted)
			{
				isLerping = true;
			}

			Target = e.SelectedUnit.gameObject;
		}

		bool LerpToTarget(Vector3 target)
		{
			lerpTimer += Time.deltaTime;
			float percentComplete = lerpTimer / LerpDuration;

			transform.position = Vector3.Lerp(
						transform.position,
						target,
						percentComplete
						);

			Debug.Log( percentComplete );

			if (lerpTimer < LerpDuration)
			{
				return true;
			}
			else
			{
				lerpTimer = 0;
				return false;
			}
		}
		void MatchStartEventHandler(MatchStartEvent gameEvent)
		{
			hasMatchStarted = true;
		}

		void OnDestroy()
		{
			EventManager.Instance.RemoveListener<UnitSelectedEvent>( OnUnitSelected );
		}
	}
}