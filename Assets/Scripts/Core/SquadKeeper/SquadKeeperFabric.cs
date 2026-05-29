using Battler;
using Battler.Core.SquadKeeping;
using System.Collections.Generic;
using UnityEngine;
using YG;

public static class SquadKeeperFabric
{
    public static GameSquadKeeper Create(StartSquadsConfig squadSet, List<SquadPlan> squadPlans)
    {
        Dictionary<string, SquadPlan> squadPlansId = GetAllId(squadPlans);
        List<GameSquadCell> squads = new();

        foreach (SquadConfig cell in squadSet.Squads)
            squads.Add(new GameSquadCell(cell.Squad, cell.Count));

        GameSquadKeeper gameSquadKeeper = new(squads);

        if (YG2.saves.boughtSquads.Count > 0)
        {
            foreach (string squadId in YG2.saves.boughtSquads)
            {
                gameSquadKeeper.AddSquad(new GameSquadCell(squadPlansId[squadId], 1));
            }
        }

        return gameSquadKeeper;
    }

    private static Dictionary<string, SquadPlan> GetAllId(List<SquadPlan> squadPlans)
    {
        Dictionary<string, SquadPlan> squadPlansId = new();

        foreach (SquadPlan plan in squadPlans)
            squadPlansId.Add(plan.Id, plan);

        return squadPlansId;
    }
}
