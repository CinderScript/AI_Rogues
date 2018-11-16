using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace IronGrimoire.Gui
{
	[RequireComponent( typeof( Animator ) )]
	[RequireComponent( typeof( CanvasGroup ) )]
	public class GUIScreen : MonoBehaviour
	{
		[Header( "Main Properties" )]
		public Selectable FirstSelectedSelectable;

		[Header( "Events" )]
		public UnityEvent OnClosed = new UnityEvent();
		public UnityEvent OnOpened = new UnityEvent();

		public GUISystem GUISystem { get; private set; }

		//private CanvasGroup canvasGroup;
		private Animator animator;
		private AudioSource[] screenAudio;

		protected virtual void Awake()
		{
			GUISystem = GetComponentInParent<GUISystem>();
			animator = GetComponent<Animator>();
			screenAudio = GetComponents<AudioSource>();

			//canvasGroup = GetComponent<CanvasGroup>();
		}
		protected virtual void Start() { }

		public virtual void CloseScreen()
		{
			// this is done via animation controller
			//canvasGroup.interactable = false;
			//canvasGroup.blocksRaycasts = false;

			OnClosed?.Invoke();
			animator.SetTrigger( "hide" );
		}
		public virtual void OpenScreen()
		{
			gameObject.SetActive( true );

			OnOpened?.Invoke();
			if (screenAudio != null)
			{
				for (int i = 0; i < screenAudio.Length; i++)
				{
					screenAudio[i].Play();
				}
			}

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