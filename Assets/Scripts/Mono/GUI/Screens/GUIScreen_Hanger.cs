using System.Collections.Generic;

using AIRogue.GameObjects;

using UnityEngine;

namespace IronGrimoire.Gui.Game
{
	class GUIScreen_Hanger : GUIScreen
	{
		[Header( "Hanger Screen Properties" )]
		public int MyProperty;

		protected override void Start()
		{
			base.Start();
		}
		public void Setup(Unit unit)
		{
			
		}
	}
}