using AIRogue.GameObjects;

using IronGrimoire.Gui;

using UnityEngine.UI;

namespace AIRogue.Gui
{
	class ControlGroup_ShipInfo : ControlGroup
	{
		public Text Name = null;
		public Text Shield = null;
		public Text Hull = null;
		public Text Velocity = null;
		public Text Accel = null;
		public Text Turn = null;
		public Text Value = null;

		public void SetText(Unit ship)
		{
			var name = ship.DisplayName;
			var shield = ship.ShieldCapacity	.ToString( "0.#" );
			var hull = ship.Health				.ToString("0.#");
			var velocity = ship.MaxVelocity		.ToString("0.#");
			var accel = ship.AccelerationForce	.ToString( "0.#" );
			var turn = ship.RotationSpeed		.ToString( "0.#" );
			var value = ship.Value				.ToString("$#");

			SetFieldValue( Name, name );
			SetFieldValue( Shield, shield );
			SetFieldValue( Hull, hull );
			SetFieldValue( Velocity, $"{velocity} m/s" );
			SetFieldValue( Accel, $"{accel} m/s/s" );
			SetFieldValue( Turn, $"{turn} deg/s" );
			SetFieldValue( Value, value );
		}
	}
}