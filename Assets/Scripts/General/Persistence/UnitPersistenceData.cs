using System.Collections.Generic;

using AIRogue.GameObjects;

using ProtoBuf;

namespace IronGrimoire.Persistence
{
	[ProtoContract]
	class UnitPersistenceData
	{
		[ProtoMember( 10 )]
		public UnitType UnitType;

		[ProtoMember( 20 )]
		public List<WeaponName> Weapons { get; }

		public UnitPersistenceData()
		{
			UnitType = UnitType.Not_Found;
			Weapons = new List<WeaponName>();
		}
		public UnitPersistenceData(Unit unit)
		{
			UnitType = unit.UnitType;
			Weapons = new List<WeaponName>();
			foreach (var weap in unit.Weapons)
			{
				Weapons.Add( weap.WeaponName );
			}
		}
	}
}