using UnityEngine;

namespace AIRogue.GameObjects {

	/// <summary>
	/// A gameplay unit used in Pawn of Kings.
	/// </summary>
	class DecalFader : MonoBehaviour
	{
		public AnimationCurve AlphaFadeCurve;

		private Material decalMat;    // color change on hit
		private float fadeLength;		// set by animation curve
		private float fadeTimestamp = 0;

		protected void Start() {

			decalMat = GetComponentInChildren<Renderer>().material;

			int lastKey = AlphaFadeCurve.length - 1;
			fadeLength = AlphaFadeCurve[lastKey].time;
		}
		protected void Update()
		{
			float alpha = AlphaFadeCurve.Evaluate( fadeTimestamp );
			setAlpha( alpha );

			fadeTimestamp += Time.deltaTime;
		}

		private void setAlpha(float alpha)
		{
			Color color = decalMat.GetColor( "_TintColor" );
			color.a = alpha;
			decalMat.SetColor( "_TintColor", color);
		}
	}
}