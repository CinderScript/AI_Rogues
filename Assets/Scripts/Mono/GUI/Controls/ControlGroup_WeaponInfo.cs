using AIRogue.GameObjects;
using UnityEngine;
using UnityEngine.UI;

namespace IronGrimoire.Gui.Game
{
	class ControlGroup_WeaponInfo : MonoBehaviour
	{
		public Text Name = null;
		public Text Type = null;
		public Text Damage = null;
		public Text Range = null;
		public Text Velocity = null;
		public Text RateOfFire = null;
		public Text Value = null;

		public void SetText(Weapon weapon)
		{
			var name = weapon.WeaponName			.ToString();
			var type = weapon.WeaponType_GUI;
			var damage = weapon.Damage				.ToString("0.###");
			var range = weapon.Range				.ToString("0.###");
			var velocity = weapon.DamagerVelocity	.ToString( "0.###" );
			var rateOfFire = weapon.RateOfFire		.ToString( "0.###" );
			var value = weapon.Value				.ToString("$");

			SetFieldValue( Name, name );
			SetFieldValue( Type, type );
			SetFieldValue( Damage, damage );
			SetFieldValue( Range, range );
			SetFieldValue( Velocity, velocity );
			SetFieldValue( RateOfFire, rateOfFire );
			SetFieldValue( Value, value );
		}

		private void SetFieldValue(Text field, string value)
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
					}
				}
			}

			fieldValueHolder.text = value.ToString();
		}
		private bool isFieldValueHolder(GameObject child)
		{
			if (child.name.Equals("text", System.StringComparison.OrdinalIgnoreCase))
			{
				return true;
			}
			if (child.name.Equals( "value", System.StringComparison.OrdinalIgnoreCase ))
			{
				return true;
			}

			return false;
		}
	}
}