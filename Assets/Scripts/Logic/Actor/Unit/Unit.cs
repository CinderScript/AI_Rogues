using UnityEngine;

namespace AIRogue.Logic.Actor {

	/// <summary>
	/// A gameplay unit used in Pawn of Kings.
	/// </summary>
	class Unit {

		/* * * Force Assignment on instance (readonly) so controller functions cannot 
		* access these fields without them being initialized and set first.  * * */
		public UnitType Type { get; }
		public GameObject Prefab { get; }
		public Movement Movement { get; }
		public Attack Attack { get; }
		public Condition Condition { get; }

		/* * * Assigned by Squad * * */
		public int Id { get; set; }
        public GameObject GameObject { get; set; }
        public Unit Target { get; set; }

        /// <summary>
        /// Creates a new Unit and initializes the Type, Prefab, Condition, Movement, and Attack.
        /// These are readonly properties that cannot be changed when the Unit is instanced.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="prefab"></param>
        /// <param name="condition"></param>
        /// <param name="movement"></param>
        /// <param name="attack"></param>
        public Unit(UnitType type, GameObject prefab, 
            Condition condition, Movement movement, Attack attack)
        {
            this.Type = type;
            this.Prefab = prefab;
            this.Condition = condition;
            this.Movement = movement;
            this.Attack = attack;
        }
    }

    public struct Attack {

        public float Range { get; private set; }
        public float Damage { get; private set; }
        public float Cost { get; private set; }

        public Attack(float range, float damage, float cost) : this()
        {
            Range = range;
            Damage = damage;
            Cost = cost;
        }
    }

    public struct Movement {

		public float VelocityMax { get; }
        public float AccelerationForce { get; }
		public float RotationSpeed { get; }

        public Movement(float velocity, float acceleration, float rotation) : this()
        {
			VelocityMax = velocity;
			AccelerationForce = acceleration;
			RotationSpeed = rotation;
        }
    }

    public struct Condition {

        public float MaxHealth { get; private set; }
        public float DamageReduction { get; private set; }

        public bool IsAlive { get; set; }
        public float Health { get; set; }

        public Condition(float maxHealth, float damageReduction) : this()
        {
            IsAlive = true;
            MaxHealth = maxHealth;
            Health = maxHealth;
            DamageReduction = damageReduction;
        }
    }

    public enum UnitType {
        Not_Found, TestUnit, SpaceFighter
    }
}