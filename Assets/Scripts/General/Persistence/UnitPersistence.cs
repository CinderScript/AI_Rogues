using System.Collections.Generic;

using AIRogue.GameObjects;

using ProtoBuf;

namespace AIRogue.Persistence
{
	[ProtoContract]
	[System.Serializable]
	class UnitSave
	{
		[ProtoMember( 10 )]
		public UnitModel UnitModel;

		[ProtoMember( 20 )]
		public List<WeaponModel> Weapons { get; }

		public UnitSave()
		{
			UnitModel = UnitModel.Not_Found;
			Weapons = new List<WeaponModel>();
		}
		public UnitSave(Unit unit)
		{
			UnitModel = unit.UnitModel;
			Weapons = new List<WeaponModel>();
			foreach (var weap in unit.Weapons)
			{
				Weapons.Add( weap.WeaponModel );
			}
		}
		public UnitSave(UnitModel unitType, List<WeaponModel> weapons)
		{
			UnitModel = unitType;
			Weapons = weapons;
		}
	}
}