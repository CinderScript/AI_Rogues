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
}
