
using AIRogue.GameObjects;

namespace AIRogue.GameState.Battle.Behavior
{
	class AttackTargetBehavior : Behavior
	{
		private Unit target;

		public AttackTargetBehavior(Unit unit, Unit target) : base( unit )
		{
			this.target = target;
		}

		public override void FixedUpdate()
		{
			
		}

		public override void Update()
		{
			
		}
	}
}
