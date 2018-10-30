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

		[Header( "Time Manager Settings" )]
		public bool ChangeSpeedOnOpen = false;
		public float GameSpeedOnOpen = 1;
		public bool ChangeSpeedOnClose = false;
		public float GameSpeedOnClose = 1;

		public GUISystem GUISystem { get; private set; }
		//private CanvasGroup canvasGroup;
		private Animator animator;

		protected virtual void Awake()
		{
			GUISystem = GetComponentInParent<GUISystem>();
			animator = GetComponent<Animator>();
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

			if (ChangeSpeedOnClose)
			{
				TimeManager.Instance.SetGameplaySpeed( GameSpeedOnClose );
			}
		}
		public virtual void OpenScreen()
		{
			gameObject.SetActive( true );

			OnOpened?.Invoke();
			animator.SetTrigger( "show" );

			if (ChangeSpeedOnOpen)
			{
				TimeManager.Instance.SetGameplaySpeed( GameSpeedOnOpen );
			}
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