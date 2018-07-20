/* 
 * Author: Gregory Maynard <CinderScript@gmail.com>
 * Copyright (C) - All Rights Reserved
 */

using AIRogue.GameObjects;
using UnityEngine;

namespace AIRogue.GameState.Battle.Behavior
{

	abstract class Behavior {

		protected readonly Unit unit;
		public Behavior(Unit unit)
		{
			this.unit = unit;
		}

        public abstract void Update();
		public abstract void FixedUpdate();
	}

	class InitialBehavior : Behavior
	{
		public InitialBehavior(Unit unit) : base( unit ) { }

		public override void FixedUpdate()
		{
			
		}

		public override void Update()
		{
			
		}
	}

	class InputListenerBehavior : Behavior
	{
		private int thrustInput;

		public InputListenerBehavior(Unit unit) : base( unit ) { }

		public override void Update()
		{
			/// GET PLAYER CONTROLLER INPUT
			thrustInput = (int)Input.GetAxisRaw( "Vertical" );
			float rotationInput = Input.GetAxis( "Horizontal" );

			bool primaryAttackInput = Input.GetButton( "Fire1" );
			bool secondaryAttackInput = Input.GetButton( "Fire1" );

			/// APPLY INPUT TO UnitActionController
			if (thrustInput > 0)                                // If thrusting
			{
				unit.ForwardThrust();
			}
			else if (thrustInput < 0 && rotationInput == 0)     // If ReversTurning and not rotating
			{
				unit.ReverseTurn();
			}

			if (rotationInput != 0)                             // If rotating
			{
				unit.Rotate( rotationInput );
			}

			if (primaryAttackInput)
			{
				unit.FireWeapons();
			}

			if (secondaryAttackInput)
			{
				unit.FireWeapons();
			}
		}
		public override void FixedUpdate()
		{
		}
	}
}