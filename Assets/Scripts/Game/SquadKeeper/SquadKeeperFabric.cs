using System.Collections.Generic;

public static class SquadKeeperFabric
{
    public static SquadKeeper Create(StartSquadsConfig squadSet)
    {
        Dictionary<SquadPlan, int> squads = new Dictionary<SquadPlan, int>();

        foreach(SquadConfig cell in squadSet.Squads)
            squads.Add(cell.Squad, cell.Count);

        return new SquadKeeper(squads);
    }
}
