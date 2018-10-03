using UnityEngine;
using UnityEngine.UI;

namespace IronGrimoire.Gui
{
	public class DialogBox : MonoBehaviour
	{
		private string _message;
		public string Message
		{
			get {
				return _message;
			}
			set {
				_message = value;
				messageField.text = value;
			}
		}

		private Text messageField;
		private Button ok;

		void Awake()
		{
			var fields = GetComponentsInChildren<Text>();
			foreach (var text in fields)
			{
				if (text.gameObject.name.Equals( "message", System.StringComparison.OrdinalIgnoreCase ))
				{
					messageField = text;
				}
			}

			var buttons = GetComponentsInChildren<Button>();
			foreach (var btn in buttons)
			{
				if (btn.gameObject.name.Equals( "OK", System.StringComparison.OrdinalIgnoreCase ) ||
					btn.gameObject.name.Equals( "OK_button", System.StringComparison.OrdinalIgnoreCase ))
				{
					ok = btn;
				}
			}

			ok.onClick.AddListener( OnOK );

			gameObject.SetActive( false );
		}

		public void Show(string message)
		{
			Message = message;
			BringToFront();
			gameObject.SetActive( true );
		}

		void OnOK()
		{
			gameObject.SetActive( false );
		}

		void BringToFront()
		{
			transform.SetAsLastSibling();
		}
	}
}