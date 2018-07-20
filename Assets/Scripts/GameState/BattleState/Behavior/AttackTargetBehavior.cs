
using AIRogue.GameObjects;
using UnityEngine;

namespace AIRogue.GameState.Battle.Behavior
{
	class AttackTargetBehavior : UnitBehavior
	{
		public Unit Target { get; set; }

		public AttackTargetBehavior(Unit unit, Unit target) : base( unit )
		{
			Target = target;
		}

		public override void FixedUpdate()
		{
			
		}

		public override void Update()
		{
			
		}
	}
}
