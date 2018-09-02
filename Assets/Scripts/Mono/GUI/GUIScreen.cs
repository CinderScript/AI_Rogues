using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace IronGrimoire.GuiBase
{
	[RequireComponent(typeof(Animator))]
	[RequireComponent(typeof(CanvasGroup))]
	public class GUIScreen : MonoBehaviour
	{
		[Header( "Main Properties" )]
		public Selectable FirstSelectedSelectable;

		[Header( "Events" )]
		public UnityEvent OnClosed = new UnityEvent();
		public UnityEvent OnOpened = new UnityEvent();

		private CanvasGroup canvasGroup;
		private Animator animator; 

		void Awake()
		{
			animator = GetComponent<Animator>();
			canvasGroup = GetComponent<CanvasGroup>();
		}

		public void CloseScreen()
		{
			// this is done via animation controller
			//canvasGroup.interactable = false;
			//canvasGroup.blocksRaycasts = false;

			OnClosed?.Invoke();
			animator.SetTrigger( "hide" );
		}
		public void OpenScreen()
		{
			gameObject.SetActive( true );

			OnOpened?.Invoke();
			animator.SetTrigger( "show" );
		}

		public void OnHideAnimation_Finished()
		{
			gameObject.SetActive( false );
		}
		public void OnShowAnimation_Finished()
		{
			SelectFirstSelectable();
		}

		void SelectFirstSelectable()
		{
			if (FirstSelectedSelectable)
			{
				EventSystem.current.SetSelectedGameObject( FirstSelectedSelectable.gameObject );
				//SelectOnStartup.OnSelect(null);
			}
		}
	}
}