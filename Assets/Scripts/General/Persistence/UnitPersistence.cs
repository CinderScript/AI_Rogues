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
		public List<WeaponName> Weapons { get; }

		public UnitPersistence()
		{
			UnitType = UnitType.Not_Found;
			Weapons = new List<WeaponName>();
		}
		public UnitPersistence(Unit unit)
		{
			UnitType = unit.UnitType;
			Weapons = new List<WeaponName>();
			foreach (var weap in unit.Weapons)
			{
				Weapons.Add( weap.WeaponName );
			}
		}
		public UnitPersistence(UnitType unitType, List<WeaponName> weapons)
		{
			UnitType = unitType;
			Weapons = weapons;
		}
	}
}