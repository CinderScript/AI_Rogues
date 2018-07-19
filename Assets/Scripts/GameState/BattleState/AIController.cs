using AIRogue.GameState.Battle.Behavior;

namespace AIRogue.GameState.Battle
{
	/// <summary>
	/// Implements UnitController and used by ArmyManager as a possible Generic type.
	/// This is a controller for AI units and its behaviors will not be choosen 
	/// through the GUI, but through AI logic.
	/// </summary>
	class AIController : UnitController {

		protected override void setInitialBehavior()
		{
			behavior = new InitialBehavior( Unit );
		}

		protected void ChooseBehavior()
		{
			// if under attack, attack back
			// else if squad member is under attack, attack back
			// else if not under attack, look for enemies in range and attack if threat level is low enough
		}

		public override void Update()
		{
			base.Update();
		}
	}
}