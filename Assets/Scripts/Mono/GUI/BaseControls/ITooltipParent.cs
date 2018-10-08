using UnityEngine;

namespace IronGrimoire.Gui
{
	public interface ITooltipParent
	{
		RectTransform InstantiatedTooltip { get; }
	}
}