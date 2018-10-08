using AIRogue.GameObjects;
using UnityEngine;
using UnityEngine.UI;

namespace IronGrimoire.Gui.Game
{
	class ControlGroup_WeaponInfo : ControlGroup
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
			if (weapon)
			{
				var name = weapon.DisplayName;
				var type = weapon.WeaponType;
				var damage = weapon.Damage				.ToString("0.###");
				var range = weapon.Range				.ToString("0.###");
				var velocity = weapon.DamagerVelocity	.ToString( "0.###" );
				var rateOfFire = weapon.RateOfFire		.ToString( "0.###" );
				var value = weapon.Value				.ToString( "$0" );

				SetFieldValue( Name, name );
				SetFieldValue( Type, type );
				SetFieldValue( Damage, damage );
				SetFieldValue( Range, range );
				SetFieldValue( Velocity, velocity );
				SetFieldValue( RateOfFire, rateOfFire );
				SetFieldValue( Value, value );
			}
			else
			{
				SetFieldValue( Name, "none selected" );
				SetFieldValue( Type, "" );
				SetFieldValue( Damage, "" );
				SetFieldValue( Range, "" );
				SetFieldValue( Velocity, "" );
				SetFieldValue( RateOfFire, "" );
				SetFieldValue( Value, "" );
			}
		}
	}
}