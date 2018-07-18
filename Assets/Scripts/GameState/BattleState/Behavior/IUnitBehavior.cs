/* 
 * Author: Gregory Maynard <CinderScript@gmail.com>
 * Copyright (C) - All Rights Reserved
 */

using AIRogue.GameObjects;
using UnityEngine;

namespace AIRogue.GameState.Battle.Behavior
{

	abstract class IUnitBehavior {

		protected readonly Unit actionController;
		public IUnitBehavior(Unit actionController)
		{
			this.actionController = actionController;
		}

        public abstract void Perform();
    }

	class InputListenerBehavior : IUnitBehavior
	{
		public InputListenerBehavior(Unit actionController) : base( actionController ) { }

		public override void Perform()
		{
			/// GET PLAYER CONTROLLER INPUT
			int thrustInput = (int)Input.GetAxisRaw( "Vertical" );
			float rotationInput = Input.GetAxis( "Horizontal" );

			bool primaryAttackInput = Input.GetButton( "Fire1" );
			bool secondaryAttackInput = Input.GetButton( "Fire1" );

			/// APPLY INPUT TO UnitActionController
			if (thrustInput > 0)                                // If thrusting
			{
				actionController.ForwardThrust();
			}
			else if (thrustInput < 0 && rotationInput == 0)     // If ReversTurning and not rotating
			{
				actionController.ReverseTurn();
			}

			if (rotationInput != 0)                             // If rotating
			{
				actionController.Rotate( rotationInput );
			}

			if (primaryAttackInput)
			{
				actionController.FireWeapons();
			}

			if (secondaryAttackInput)
			{
				actionController.FireWeapons();
			}
		}
	}
}