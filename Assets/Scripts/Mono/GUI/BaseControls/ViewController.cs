using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace IronGrimoire.Gui
{
	[RequireComponent(typeof(GUIScreen))]
	public abstract class ViewController : MonoBehaviour
	{
		[Header( "On Key/Button Down Event Handlers" )]
		public List<KeyDownEvent> OnKeyDownHandlers;
		public List<ButtonDownEvent> OnButtonDownHandlers;

		[Header( "Time Manager Settings" )]
		public bool ChangeSpeedOnOpen = false;
		public float GameSpeedOnOpen = 1;
		public bool ChangeSpeedOnClose = false;
		public float GameSpeedOnClose = 1;

		public GUISystem GUISystem { get; private set; }
		public GUIScreen GUIScreen { get; private set; }

		protected virtual void Awake()
		{
			GUISystem = GetComponentInParent<GUISystem>();
			GUIScreen = GetComponent<GUIScreen>();

			GUIScreen.OnOpened.AddListener( UpdateView );

			if (ChangeSpeedOnOpen)
			{
				GUIScreen.OnOpened.AddListener( ChangeOpenedSpeed );
			}
			if (ChangeSpeedOnClose)
			{
				GUIScreen.OnClosed.AddListener( ChangeClosedSpeed );
			}
		}
		protected virtual void Start() { }

		public abstract void UpdateView();

		void Update()
		{
			// run "Invoke If" method for each key down handler in list
			for (int i = 0; i < OnButtonDownHandlers.Count; i++)
			{
				OnButtonDownHandlers[i].InvokeIfButtonDown();
			}
			for (int i = 0; i < OnKeyDownHandlers.Count; i++)
			{
				OnKeyDownHandlers[i].InvokeIfKeyDown();
			}
		}
		void ChangeOpenedSpeed()
		{
			TimeManager.Instance.SetGameplaySpeed( GameSpeedOnOpen );
		}
		void ChangeClosedSpeed()
		{
			TimeManager.Instance.SetGameplaySpeed( GameSpeedOnClose );
		}
	}

	[System.Serializable]
	public class KeyDownEvent
	{
		public KeyCode Key;
		public UnityEvent Handler;

		public void InvokeIfKeyDown()
		{
			if (Input.GetKeyDown( Key ))
			{
				Handler?.Invoke();
			}
		}
	}

	[System.Serializable]
	public class ButtonDownEvent
	{
		public string Button;
		public UnityEvent Handler;

		public void InvokeIfButtonDown()
		{
			if (Input.GetButtonDown( Button ))
			{
				Handler?.Invoke();
			}
		}
	}
}