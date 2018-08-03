using System.Collections.Generic;
using AIRogue.GameObjects;

namespace AIRogue.Events {

    class UnitSelectedEvent : GameEvent
	{
		public Unit SelectedUnit { get; }

		public UnitSelectedEvent(Unit selectedUnit)
		{
			SelectedUnit = selectedUnit;
		}
	}

	class UnitDestroyedEvent : GameEvent
	{
		public Unit Unit { get; }

		public UnitDestroyedEvent(Unit destroyedUnit)
		{
			Unit = destroyedUnit;
		}
	}

	class BattleStartEvent : GameEvent
	{
		public List<Unit> Units { get; }

		public BattleStartEvent(List<Unit> units)
		{
			Units = units;
		}
	}
}
