using System;

namespace AIRogue.Exceptions
{
	public class UnitComponentNotAttachedException : Exception
	{
		public UnitComponentNotAttachedException()
		{
		}

		public UnitComponentNotAttachedException(string message)
			: base( message )
		{
		}

		public UnitComponentNotAttachedException(string message, Exception inner)
			: base( message, inner )
		{
		}
	}

	public class WeaponComponentNotAttachedException : Exception
	{
		public WeaponComponentNotAttachedException()
		{
		}

		public WeaponComponentNotAttachedException(string message)
			: base( message )
		{
		}

		public WeaponComponentNotAttachedException(string message, Exception inner)
			: base( message, inner )
		{
		}
	}

	public class DamageComponentNotAttachedException : Exception
	{
		public DamageComponentNotAttachedException()
		{
		}

		public DamageComponentNotAttachedException(string message)
			: base( message )
		{
		}

		public DamageComponentNotAttachedException(string message, Exception inner)
			: base( message, inner )
		{
		}
	}
}
