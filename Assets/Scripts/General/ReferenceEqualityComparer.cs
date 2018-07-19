using System.Collections.Generic;

namespace General
{
	public sealed class ReferenceEqualityComparer<TKey> : IEqualityComparer<TKey>
		where TKey : class
	{
		/// <inheritdoc />
		public bool Equals(TKey left, TKey right)
		{
			return ReferenceEquals( left, right );
		}

		/// <inheritdoc />
		public int GetHashCode(TKey value)
		{
			return System.Runtime.CompilerServices.RuntimeHelpers.GetHashCode( value );
		}
	}
}