using UnityEngine;

namespace AIRogue.GameObjects {

	/// <summary>
	/// A gameplay unit used in Pawn of Kings.
	/// </summary>
	class DecalFader : MonoBehaviour
	{
		public string ShaderPropertyName = "_TintColor";
		public AnimationCurve AlphaFadeCurve;

		private Material decalMat;		// color change on hit
		private float fadeTimestamp = 0;

		protected void Start()
		{
			decalMat = GetComponentInChildren<Renderer>().material;
		}
		protected void Update()
		{
			float alpha = AlphaFadeCurve.Evaluate( fadeTimestamp );
			setAlpha( alpha );

			fadeTimestamp += Time.deltaTime;
		}

		private void setAlpha(float alpha)
		{
			Color color = decalMat.GetColor( ShaderPropertyName );
			color.a = alpha;
			decalMat.SetColor( ShaderPropertyName, color);
		}
	}
}