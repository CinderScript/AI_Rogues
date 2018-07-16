using UnityEngine;

namespace AIRogue.GameObjects {

	/// <summary>
	/// A gameplay unit used in Pawn of Kings.
	/// </summary>
	class SphereShield : Shield
	{
		private const float FLASH_TIME_LENGTH = 0.15f;

		private Collider col;
		private Material mat;
		private float alphaMax;

		protected override void Awake()
		{
			base.Awake();

			col = GetComponentInChildren<Collider>();
			mat = GetComponentInChildren<Renderer>().material;
			alphaMax = mat.color.a;
		}
		protected override void Start()
		{
			base.Start();
		}
		protected override void Update()
		{
			base.Update();
		}

		protected override void SetApperance(float percentage)
		{
			//Color last = mat.color;
		}

		protected override void ShieldOff()
		{
			col.enabled = false;
			Color color = mat.color;
			color.a = 0;
			mat.color = color;
		}
		protected override void ShieldOn()
		{
			col.enabled = true;
		}
	}
}