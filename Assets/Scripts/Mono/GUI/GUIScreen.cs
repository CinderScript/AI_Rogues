using System;

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
		public Selectable SelectOnStartup;

		[Header( "Events" )]
		public UnityEvent OnClosed = new UnityEvent();
		public UnityEvent OnOpened = new UnityEvent();

		private Animator animator;

		void Start()
		{
			animator = GetComponent<Animator>();

			if (SelectOnStartup)
			{
				EventSystem.current.SetSelectedGameObject( SelectOnStartup.gameObject );
			}
		}

		public void Close()
		{
			Debug.Log( "closed: " + gameObject );
			OnClosed?.Invoke();
			animator.SetTrigger( "hide" );
		}

		public void Open()
		{
			Debug.Log( "opened: "+gameObject );

			OnOpened?.Invoke();
			animator.SetTrigger( "show" );
		}
	}
}