using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SquadKeeperFabric
{
    public static SquadKeeper Create(StartSquadSet squadSet)
    {
        Dictionary<SquadPlan, int> squads = new Dictionary<SquadPlan, int>();

        foreach(SquadSetCell cell in squadSet.Squads)
            squads.Add(cell.Squad, cell.Count);

        return new SquadKeeper(squads);
    }
}
