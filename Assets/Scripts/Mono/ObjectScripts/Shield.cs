using UnityEngine;

namespace AIRogue.GameObjects {

	/// <summary>
	/// A gameplay unit used in Pawn of Kings.
	/// </summary>
	abstract class Shield : MonoBehaviour
	{
		public float HitPointCapacity { get; private set; }
		public float HitPoints { get; private set; }
		public float ShieldPercentage
		{
			get {
				return HitPoints / HitPointCapacity;
			}
		}

		private bool isActive = true;
		private float secSinceLastDamage; //updated each frame

		protected virtual void Awake()
		{
		}
		protected virtual void Start()
		{

		}
		protected virtual void Update()
		{
			secSinceLastDamage += Time.deltaTime;
		}

		public void Initialize(float shieldCapacity)
		{
			HitPointCapacity = shieldCapacity;
			HitPoints = shieldCapacity;
		}

		/// <summary>
		/// Damages this Shields and returns the amount of damage that 
		/// could not be absorbed.
		/// </summary>
		/// <param name="damage"></param>
		/// <returns>Through Damage</returns>
		public float TakeDamage(float damage)
		{
			var throughDamage = 0f;
			if (HitPoints > 0)
			{
				HitPoints -= damage;
				secSinceLastDamage = 0;

				if (HitPoints < 0)
				{
					throughDamage = HitPoints * -1f;
					HitPoints = 0;
				}

				SetConditionApperance();

				if (HitPoints == 0)
				{
					ShieldOff();
				}
			}
			else
			{
				throughDamage = damage;
			}

			return throughDamage;
		}

		protected abstract void SetConditionApperance();
		protected abstract void ShieldOff();
		protected abstract void ShieldOn();
	}
}