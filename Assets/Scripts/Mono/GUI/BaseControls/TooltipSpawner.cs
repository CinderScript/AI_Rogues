using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace IronGrimoire.Gui
{
	public class TooltipSpawner : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
	{
		[Header( "Tooltip Properties" )]
		public float ActivationDelay = 0.5f;
		public bool UseTooltipPivotAsOffset = true;
		public Vector2 TooltipMouseFollowOffset = new Vector2( 50, -50 );

		private ITooltipParent tooltipParent;
		private RectTransform tooltip;
		private bool isHovering;
		private bool haveTooltip;
		private Canvas canvas;
		private RectTransform canvasRect;

		void Start()
		{			
			tooltipParent = GetComponent<ITooltipParent>();
			if (tooltipParent == null)
			{
				Debug.LogWarning( $"This ToolTipSpawner could not find an attached " +
					$"ITooltipParent.\nThis spawner is attached to: {gameObject.name}" );
			}

			// run after awake in case this item is spawned before assigned a parent
			canvas = GetComponentInParent<Canvas>();
			canvasRect = canvas.GetComponent<RectTransform>();
		}

		void OnDestroy()
		{
			if (haveTooltip)
			{
				Destroy( tooltip.gameObject );
			}
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			if (!haveTooltip)
			{
				GetTooltip();
			}

			isHovering = true;

			StartCoroutine( MakeVisibleAfterDelay() );
		}
		public void OnPointerExit(PointerEventData eventData)
		{
			isHovering = false;
			tooltip.gameObject.SetActive( false );
		}

		void GetTooltip()
		{
			tooltip = tooltipParent.InstantiatedTooltip;
			tooltip.SetParent( canvasRect, false );
			tooltip.SetAsLastSibling();
			tooltip.gameObject.SetActive( false );

			haveTooltip = true;
		}

		IEnumerator MakeVisibleAfterDelay()
		{
			yield return new WaitForSecondsRealtime( ActivationDelay );

			if (isHovering)
			{
				tooltip.gameObject.SetActive( true );
				StartCoroutine( FollowMouse() );
			}
		}
		IEnumerator FollowMouse()
		{
			while (isHovering)
			{
				//set pos
				tooltip.localPosition = GetTooltipPosition();

				// wait for next frame
				yield return null;
			}
		}
		Vector2 GetTooltipPosition()
		{
			Vector2 mousePos = Input.mousePosition;
			Vector2 offsetPos;

			if (UseTooltipPivotAsOffset)
			{
				offsetPos = mousePos;
			}
			else
			{
				offsetPos = mousePos + TooltipMouseFollowOffset;
			}

			Vector2 localPoint;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(
				canvasRect,
				offsetPos, 
				canvas.worldCamera,
				out localPoint );

			return localPoint;
		}
	}
}