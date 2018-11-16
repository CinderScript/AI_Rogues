/* 
 * Author: Gregory Maynard <CinderScript@gmail.com>
 * Copyright (C) - All Rights Reserved
 */

using System;
using System.Collections.Generic;
using AIRogue.GameObjects;
using UnityEngine;

namespace AIRogue.GameState.Battle.BehaviorTree
{
	abstract class Behavior {

		protected readonly UnitController controller;
		protected readonly Unit unit;

		public Behavior(UnitController unitController)
		{
			controller = unitController;
			unit = controller.Unit;
		}
        public abstract RunnableBehavior EvaluateTree();

		protected float LookAngleToPosition(Vector3 position)
		{
			return Vector3.Angle( unit.transform.forward, position - unit.transform.position );
		}
		protected int DirectionToPosition(Vector3 position)
		{
			if (unit.transform.InverseTransformPoint( position ).x < 0)
			{
				return -1;
			}
			else
			{
				return 1;
			}
		}
		protected List<Weapon> WeaponsInRange(Vector3 position)
		{
			List<Weapon> inRange = new List<Weapon>();
			foreach (var weapon in unit.Weapons)
			{
				if (weapon.InRangeOf(position))
				{
					inRange.Add( weapon );
				}
			}

			return inRange;
		}
		protected List<Unit> AlliesInWeaponRange()
		{
			List<UnitController> allies = controller.Squad.Controllers;
			List<Unit> alliesInRange = new List<Unit>();
			Func<Vector3, bool> inRange = unit.WeaponWithLongestRange.InRangeOf;

			// for each controller in this squad
			for (int i = 0; i < allies.Count; i++)
			{
				// if unit is in range of this unit's longest range weap
				if (inRange( allies[i].Unit.transform.position ))
				{
					// if unit is not self
					if ( allies[i].Unit != unit )
					{
						alliesInRange.Add( allies[i].Unit );
					}
				}
			}

			return alliesInRange;
		}
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
				controller.InCombat = true;
				return inBattle.EvaluateTree();
			}
			else
			{
				controller.InCombat = false;
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