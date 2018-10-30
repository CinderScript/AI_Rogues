
using System.Collections;

using UnityEngine;

namespace IronGrimoire.Gui
{
	class GUITimedScreen : GUIScreen
	{
		[Header( "Timed Screen Properties" )]
		public float AppearanceDuration = 3;
		public GUIScreen NextScreen = null;

		public override void OpenScreen()
		{
			base.OpenScreen();

			StartCoroutine( LoadScreenAfterDuration() );
		}

		IEnumerator LoadScreenAfterDuration()
		{
			yield return new WaitForSecondsRealtime( AppearanceDuration );

			GUISystem.OpenScreen( NextScreen );
		}
	}
}