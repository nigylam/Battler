using Battler;
using Battler.Core.SquadKeeping;
using System.Collections.Generic;

public static class SquadKeeperFabric
{
    public static GameSquadKeeper Create(StartSquadsConfig squadSet)
    {
        List<GameSquadCell> squads = new();

        foreach(SquadConfig cell in squadSet.Squads)
            squads.Add(new GameSquadCell(cell.Squad, cell.Count));

        return new GameSquadKeeper(squads);
    }
}
