using System.Linq;
using AIRogue.GameObjects;
using AIRogue.GameState.Battle.Behavior;

namespace AIRogue.GameState.Battle
{
	/// <summary>
	/// Implements UnitController and used by ArmyManager as a possible Generic type.
	/// This is a controller for AI units and its behaviors will not be choosen 
	/// through the GUI, but through AI logic.
	/// </summary>
	class AIController : UnitController {

		protected override void SetInitialBehavior()
		{
			Behavior = new InitialBehavior( Unit );
		}
		protected void ChooseBehavior()
		{
			if (Target != null)
			{
				// attack... use delegate instead?
				if (Behavior.GetType() != typeof(AttackTargetBehavior))
				{
					Behavior = new AttackTargetBehavior( Unit, Target );
				}
			}
			else if (HasAttacker)
			{
				Target = Attackers[0];
			}
			else if (AlliesWithTargets.Length > 0)
			{
				Target = AlliesWithTargets[0].Target;
			}


			// if under attack
			//		- attack or run (ship level, health, dps)
			// else if squad member is attacking, help
			// else if not under attack, look for enemies in range and attack if threat level is low enough
			// else wander
			//		- investigate neutral ships
		}

		public override void Update()
		{
			ChooseBehavior();
			base.Update(); // updates selected behavior
		}
	}
}