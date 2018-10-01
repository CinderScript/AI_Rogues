using System.Collections.Generic;

using AIRogue.GameObjects;

using ProtoBuf;

namespace AIRogue.Persistence
{
	[ProtoContract]
	class UnitPersistence
	{
		[ProtoMember( 10 )]
		public UnitType UnitType;

		[ProtoMember( 20 )]
		public List<WeaponID> Weapons { get; }

		public UnitPersistence()
		{
			UnitType = UnitType.Not_Found;
			Weapons = new List<WeaponID>();
		}
		public UnitPersistence(Unit unit)
		{
			UnitType = unit.UnitType;
			Weapons = new List<WeaponID>();
			foreach (var weap in unit.Weapons)
			{
				Weapons.Add( weap.WeaponName );
			}
		}
		public UnitPersistence(UnitType unitType, List<WeaponID> weapons)
		{
			UnitType = unitType;
			Weapons = weapons;
		}
	}
}