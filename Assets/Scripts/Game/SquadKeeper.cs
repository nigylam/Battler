using System.Collections.Generic;

public class SquadKeeper
{
    private Dictionary<SquadPlan, int> _squads;

    public SquadKeeper(Dictionary<SquadPlan, int> squads) {  _squads = squads; }

    public IReadOnlyCollection<SquadPlan> Squads => _squads.Keys;

    public int GetSquadsCount(SquadPlan squad)
    {
        if(_squads.TryGetValue(squad, out int squadsCount))
            return squadsCount;

        return 0;
    }
}
