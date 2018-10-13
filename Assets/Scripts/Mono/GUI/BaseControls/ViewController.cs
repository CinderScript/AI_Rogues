using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace IronGrimoire.Gui
{
	[RequireComponent(typeof(GUIScreen))]
	public abstract class ViewController : MonoBehaviour
	{
		public GUISystem GUISystem { get; private set; }
		public GUIScreen GUIScreen { get; private set; }

		protected virtual void Awake()
		{
			GUISystem = GetComponentInParent<GUISystem>();
			GUIScreen = GetComponent<GUIScreen>();

			GUIScreen.OnOpened.AddListener( UpdateView );
		}
		protected virtual void Start() { }

		public abstract void UpdateView();
	}
}