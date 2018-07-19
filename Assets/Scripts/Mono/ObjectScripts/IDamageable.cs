using UnityEngine;

namespace AIRogue.GameObjects
{
	/// <summary>
	/// Used during collisions to find the componant that should have delt to it.
	/// In the collision, the first IDamageable componant is searched for in the 
	/// parent GameObject, so the colliders should be nested under this component.
	/// </summary>
	interface IDamageable
	{
		void TakeDamage(Unit attacker, float damage, Collision collision);
	}
}
