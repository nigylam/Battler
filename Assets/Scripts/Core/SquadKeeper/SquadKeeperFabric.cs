using Battler;
using Battler.Core.SquadKeeping;
using System.Collections.Generic;
using UnityEngine;
using YG;

public static class SquadKeeperFabric
{
    public static GameSquadKeeper Create(StartSquadsConfig squadSet)
    {
        List<GameSquadCell> squads = new();

        foreach(SquadConfig cell in squadSet.Squads)
            squads.Add(new GameSquadCell(cell.Squad, cell.Count));

        GameSquadKeeper gameSquadKeeper = new(squads);

        foreach (SquadPlan squad in YG2.saves.boughtSquads)
        {
            gameSquadKeeper.AddSquad(new GameSquadCell(squad, 1));
        }

        return gameSquadKeeper;
    }
}
