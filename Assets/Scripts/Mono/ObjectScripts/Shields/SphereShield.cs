using UnityEngine;

namespace AIRogue.GameObjects {

	class SphereShield : Shield
	{
		[Header( "Shield Effects" )]
		public Gradient ShieldConditionColor;
		public AnimationCurve ShieldFlashAnim;

		[Header( "Shield Hit Particle" )]
		public GameObject ShieldFlashPrefab = null;

		private Collider shieldCollider;	// turns on and off
		private Material shieldMaterial;    // color change on hit

		private CollisionEffectsManager effectSpawner;

		private bool isFlashing = false;
		private float flashTimestamp = 0;
		private float flashTimeLength;  // set by animation curve

		protected override void Awake()
		{
			base.Awake();

			shieldCollider = GetComponentInChildren<Collider>();
			shieldMaterial = GetComponentInChildren<Renderer>().material;

			int lastKey = ShieldFlashAnim.length - 1;
			flashTimeLength = ShieldFlashAnim[lastKey].time;

			setShieldAlpha( 0 );

			effectSpawner = gameObject.AddComponent<CollisionEffectsManager>();
		}
		protected override void Start()
		{
			base.Start();

			ShieldFlashPrefab = Unit.ShieldImpactEffect;
		}
		protected override void Update()
		{
			base.Update();


			flashShields();
		}

		protected override void SetConditionApperance()
		{
			Color color = ShieldConditionColor.Evaluate( ShieldPercentage );
			shieldMaterial.color = color;

			startFlash();
		}
		protected override void collisionEffect(Collision collision)
		{
			effectSpawner.SpawnEffect( ShieldFlashPrefab, collision );
		}

		protected override void ShieldOff()
		{
			shieldCollider.enabled = false;
			setShieldAlpha( 0 );
			stopFlash();
			effectSpawner.DestroyEffects();
		}
		protected override void ShieldOn()
		{
			shieldCollider.enabled = true;
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
			if (isFlashing)
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
		}
		private void setShieldAlpha(float alpha)
		{
			Color shieldColor = shieldMaterial.color;
			shieldColor.a = alpha;
			shieldMaterial.color = shieldColor;
		}
	}
}