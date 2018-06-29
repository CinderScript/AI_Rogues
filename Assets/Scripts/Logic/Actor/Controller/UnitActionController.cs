using UnityEngine;

namespace AIRogue.Logic.Actor
{
	class UnitActionController
	{
		protected Transform transform;
		protected Rigidbody rigidbody;

		protected float shipAccelerationForceVertical;
		protected float shipAccelerationForceHorizontal;
		protected float shipVelocityMax;
		protected float shipVelocityMaxSqr;
		protected float shipRotationSpeed;

		private const float SIDETHRUST_SCALER = 0.6f;

		public UnitActionController(Unit unit)
		{
			transform = unit.GameObject.transform;
			rigidbody = unit.GameObject.GetComponent<Rigidbody>();
			
			shipAccelerationForceVertical = unit.Movement.AccelerationForce;
			shipAccelerationForceHorizontal = unit.Movement.AccelerationForce * SIDETHRUST_SCALER;
			shipVelocityMax = unit.Movement.VelocityMax;
			shipVelocityMaxSqr = unit.Movement.VelocityMax * unit.Movement.VelocityMax;
			shipRotationSpeed = unit.Movement.RotationSpeed;
		}

		/// <summary>
		/// Applies a force in the forward vector to the ridgidbody of the ship.
		/// Ship will not exceed it's maxVelocity.
		/// Should be called in FixedUpdate().
		/// </summary>
		protected void ForwardThrust()
		{
			rigidbody.AddRelativeForce( Vector3.forward * shipAccelerationForceVertical );

			// Velocity Cap
			if (rigidbody.velocity.sqrMagnitude >= shipVelocityMaxSqr)
			{
				rigidbody.velocity = rigidbody.velocity.normalized * shipVelocityMax;  // preservs directional motion
			}
		}

		/// <summary>
		/// Applies a force in the horizonal vector to the ridgidbody of the ship.
		/// Ship will not exceed it's maxVelocity.
		/// Should be called in FixedUpdate().
		/// </summary>
		protected void SideThrust(float inputDirection)
		{
			rigidbody.AddRelativeForce( Vector3.right * shipAccelerationForceHorizontal * inputDirection );

			// Velocity Cap
			if (rigidbody.velocity.sqrMagnitude >= shipVelocityMaxSqr)
			{
				rigidbody.velocity = rigidbody.velocity.normalized * shipVelocityMax;  // preservs directional motion
			}
		}

		/// <summary>
		/// Rotates the Ship to face the opposite direction of travel.
		/// Does not activate if ship velocity = 0;
		/// </summary>
		protected void ReverseTurn()
		{
			if (rigidbody.velocity.sqrMagnitude != 0)
			{
				transform.rotation = Quaternion.RotateTowards( transform.rotation,   // rotate from current ship rotation
										 Quaternion.LookRotation( -rigidbody.velocity ),  // to rotation taken from -velocity
										 shipRotationSpeed * Time.deltaTime );
			}
		}

		/// <summary>
		/// Rotates the ship around the y-axis.
		/// </summary>
		/// <param name="ship">Object to rotate</param>
		/// <param name="rotationVelocity">Velocity</param>
		/// <param name="inputDirection">Positive values rotate right, negative left</param>
		protected void Rotate(float inputDirection)
		{
			transform.Rotate( 0, shipRotationSpeed * Time.deltaTime * inputDirection, 0 );
		}
	}
}