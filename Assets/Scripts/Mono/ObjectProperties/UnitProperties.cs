using UnityEngine;

using AIRogue.Logic.Actor;

namespace AIRogue.Unity.ObjectProperties {

    /// <summary>
    /// Contains all the properties needed to define a Unit in the Unity Editor.  Place this script 
    /// on a prefab and set the values.  The created prefab can then be added to the UnitBank MonoBehaviour.
    /// </summary>
    class UnitProperties : MonoBehaviour {

        [Header( "Game Object Resource" )]
        public GameObject Prefab = null;

        [Header( "Unit Values" )]
        public UnitType UnitType = UnitType.Not_Found;

        [Header( "Condition" )]
        public float MaxHealth = 1;
        public float Armour = 1;

        [Header( "Attack" )]
        public float AttackRange = 1;
        public float AttackDamage = 1;
        public float AttackCost = 1;

        [Header( "Movement" )]
        public float MaxVelocity = 1;
        public float AccelerationForce = 1;
        public float RotationSpeed = 1;
    }
}