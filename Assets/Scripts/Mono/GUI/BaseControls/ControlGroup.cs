using UnityEngine;
using UnityEngine.UI;

namespace IronGrimoire.Gui
{
	public class ControlGroup : MonoBehaviour
	{
		protected void SetFieldValue(Text field, string value)
		{
			Text fieldValueHolder = field;

			// if the Text object has children, check to see if one of its
			// children should hold the value. (for FieldLabel: Value formatting)
			Text[] texts = field.GetComponentsInChildren<Text>();
			if (texts.Length > 0)
			{
				foreach (var text in texts)
				{
					if (isFieldValueHolder(text.gameObject	))
					{
						fieldValueHolder = text;
						break;
					}
				}
			}

			fieldValueHolder.text = value.ToString();
		}
		protected bool isFieldValueHolder(GameObject child)
		{
			if (child.name.Equals("text"))
			{
				return true;
			}
			if (child.name.Equals( "value" ))
			{
				return true;
			}

			return false;
		}
	}
}