using UnityEngine;

namespace AIRogue.GameObjects {

	/// <summary>
	/// A gameplay unit used in Pawn of Kings.
	/// </summary>
	class SphereShield : Shield
	{
		//public Gradient ShieldConditionColor;
		public AnimationCurve ShieldFlashAnim;
		
		public Color StrongShieldColor;
		public Color WeakShieldColor;

		private const float ALPHA_MAX = 0.6f;

		private Collider collider;			// turns on and off
		private Material shieldMaterial;    // color change on hit

		private bool isFlashing = false;
		private float flashTimestamp = 0;
		private float flashTimeLength;  // set by animation curve

		protected override void Awake()
		{
			base.Awake();

			collider = GetComponentInChildren<Collider>();
			shieldMaterial = GetComponentInChildren<Renderer>().material;
		}
		protected override void Start()
		{
			base.Start();

			int lastKey = ShieldFlashAnim.length - 1;
			flashTimeLength = ShieldFlashAnim[lastKey].time;
		}
		protected override void Update()
		{
			base.Update();

			if (isFlashing)
			{
				flashShields();
			}
		}

		protected override void SetConditionApperance()
		{
			Color color = Color.Lerp( WeakShieldColor, StrongShieldColor, ShieldPercentage );
			shieldMaterial.color = color;

			startFlash();
		}

		protected override void ShieldOff()
		{
			collider.enabled = false;
			setShieldAlpha( 0 );
			stopFlash();
		}
		protected override void ShieldOn()
		{
			collider.enabled = true;
		}

		private void startFlash()
		{
			isFlashing = true;
			flashTimestamp = 0;
		}
		private void stopFlash()
		{
			flashTimestamp = 0;
			setShieldAlpha( 0 );
			isFlashing = false;
		}
		private void flashShields()
		{
			float alpha = ShieldFlashAnim.Evaluate( flashTimestamp );
			setShieldAlpha( alpha );

			flashTimestamp += Time.deltaTime;

			// if flash is finished
			if (flashTimestamp > flashTimeLength)
			{
				stopFlash();
			}
		}
		private void setShieldAlpha(float alpha)
		{
			Color shieldColor = shieldMaterial.color;
			shieldColor.a = alpha;
			shieldMaterial.color = shieldColor;
		}
	}
}