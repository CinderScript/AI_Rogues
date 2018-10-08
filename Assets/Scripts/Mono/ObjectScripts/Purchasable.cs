using AIRogue.Exceptions;

using UnityEngine;

namespace AIRogue.GameObjects
{
	/// <summary>
	/// A gameplay unit used in Pawn of Kings.
	/// </summary>
	abstract class Purchasable : MonoBehaviour
	{
		[Header( "Purchasable - GUI" )]
		public Sprite Icon;

		[Header( "Purchasable - Properties" )]
		public int Value;

		/// <summary>
		/// Name to be used when displaying item in the GUI.
		/// </summary>
		public abstract string DisplayName { get; }
	}
}