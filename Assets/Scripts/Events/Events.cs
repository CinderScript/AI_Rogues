using System.Collections.Generic;
using AIRogue.GameObjects;
using AIRogue.GameState.Battle;

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

	class UnitsSpawnedEvent : GameEvent
	{
		public List<Unit> EnemyUnits { get; }
		public List<Unit> PlayerUnits { get; }

		public UnitsSpawnedEvent(List<Squad> squads)
		{
			EnemyUnits = new List<Unit>();
			PlayerUnits = new List<Unit>();

			// for each unit in the list of squads, divide up player and AI factioned units
			foreach (var squad in squads)
			{
				if (squad.Faction == SquadFaction.Player)
				{
					foreach (var controller in squad.Controllers)
					{
						PlayerUnits.Add( controller.Unit );
					}
				}
				else
				{
					foreach (var controller in squad.Controllers)
					{
						EnemyUnits.Add( controller.Unit );
					}
				}

			}
		}
	}
}
