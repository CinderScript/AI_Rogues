namespace AIRogue.GameState.Battle.BehaviorTree
{
	class InBattleBehavior : Behavior
	{
		AttackTargetBehavior attackTarget;
		//HelpAllyBehavior helpAlly;

		public InBattleBehavior(UnitController controller) : base( controller )
		{
			attackTarget = new AttackTargetBehavior( controller );
			//helpAlly = new HelpAllyBehavior( controller );
		}

		public override RunnableBehavior EvaluateTree()
		{
			return attackTarget.EvaluateTree();
		}
	}
}