using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIRogue.GameObjects;
using ProtoBuf;

namespace IronGrimoire.Persistence
{
	[ProtoContract]
	class PlayerInfo
	{
		[ProtoMember(10)]
		public string MyString { get; set; }

		[ProtoMember( 20 )]
		public UnitPersistenceData PlayerShip { get; set; }
	}
}
