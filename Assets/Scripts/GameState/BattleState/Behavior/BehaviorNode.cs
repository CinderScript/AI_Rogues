/* 
 * Author: Gregory Maynard <CinderScript@gmail.com>
 * Copyright (C) - All Rights Reserved
 */

using AIRogue.GameObjects;
using UnityEngine;

namespace AIRogue.GameState.Battle.BehaviorTree
{
	abstract class Behavior {

		protected readonly UnitController controller;

		public Behavior(UnitController unitController)
		{
			controller = unitController;
		}

        public abstract RunnableBehavior EvaluateTree();
	}
	
		// CONDITION: was damaged, alli was damaged, finds enemy
			// DESCISION: in battle
				// CONDITION: Has healing abilities, alli health, has target
					// fight targets
						// ActionBehavior
					// support allies
					// run
			// DESCISION: out of battle
				// CONDITION: Has healing abilities, alli health, 
					// look for enemies
					// help allies
						//
					// follow leader
		
			// look for target
			// investigate target
			// attack target

	class AIBehaviorRoot : Behavior {
		// CONDITION: was damaged, alli was damaged, finds enemy
			//GetCondition List (for ai)
		// possible descisions
		InBattleBehavior inBattle;
		OutOfBattleBehavior outBattle;

		public AIBehaviorRoot(UnitController controller) : base( controller )
		{
			inBattle = new InBattleBehavior( controller );
			outBattle = new OutOfBattleBehavior( controller );
		}

		public override RunnableBehavior EvaluateTree()
		{
			if (controller.Target != null)
			{
				return inBattle.EvaluateTree();
			}
			else
			{
				return outBattle.EvaluateTree();
			}
		}
	}


	class InputListenerBehavior : RunnableBehavior
	{

		public InputListenerBehavior(UnitController controller) : base( controller ) { }

		public override RunnableBehavior EvaluateTree()
		{
			return this;
		}
		protected override UnitActions UpdateActions()
		{
			/// GET PLAYER CONTROLLER INPUT
			int thrustInput = (int)Input.GetAxisRaw( "Vertical" );
			float rotationInput = Input.GetAxis( "Horizontal" );

			bool primaryAttackInput = Input.GetButton( "Fire1" );
			bool secondaryAttackInput = Input.GetButton( "Fire1" );

			return new UnitActions(thrustInput, rotationInput, primaryAttackInput, secondaryAttackInput);
		}
	}
}