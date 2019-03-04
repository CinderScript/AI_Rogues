using System.Collections;
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
		public GameObject SelectedUnitIndicator; 
		public float LerpDuration = 0.5f;

		private bool isLerping = false;
		private float lerpTimer = 0;
		private bool hasMatchStarted = false;

		private GameObject selectedUnitIndicator;

		void Awake()
		{
			EventManager.Instance.AddListener<PlayerLeaderChangedEvent>( OnPlayerLeaderChangedEvent );
			EventManager.Instance.AddListenerOnce<MatchStartEvent>( MatchStartEventHandler );
		}

		void Start()
		{
			selectedUnitIndicator = Instantiate( SelectedUnitIndicator );
		}

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

				selectedUnitIndicator.transform.position = Target.transform.position;
			}
			else
			{
				selectedUnitIndicator.transform.position = Vector3.zero;
			}
		}

		// Game Event Listener
		void OnPlayerLeaderChangedEvent(PlayerLeaderChangedEvent e)
		{
			if (hasMatchStarted)
			{
				isLerping = true;
			}

			Target = e.SelectedUnit.gameObject;

			// set indicator size
			var shipSize = Target.GetComponentInChildren<Renderer>().bounds.size;
			var width = shipSize.x + 4;
			selectedUnitIndicator.GetComponent<SpriteRenderer>().size = new Vector2( width, width );
			StartCoroutine( FlashSelectedUnitIndicator() );
		}
		void MatchStartEventHandler(MatchStartEvent gameEvent)
		{
			hasMatchStarted = true;
		}

		bool LerpToTarget(Vector3 target)
		{
			lerpTimer += Time.deltaTime;
			float percentComplete = lerpTimer / LerpDuration;

			Debug.Log( percentComplete );

			transform.position = Vector3.Lerp(
						transform.position,
						target,
						percentComplete
						);

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
		IEnumerator FlashSelectedUnitIndicator()
		{
			for (int i = 0; i < 5; i++)
			{
				selectedUnitIndicator.SetActive( true );
				yield return new WaitForSecondsRealtime( 0.5f );
				selectedUnitIndicator.SetActive( false );
				yield return new WaitForSecondsRealtime( 0.3f );
			}
		}

		void OnDestroy()
		{
			EventManager.Instance.RemoveListener<PlayerLeaderChangedEvent>( OnPlayerLeaderChangedEvent );
		}
	}
}