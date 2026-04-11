using System.Collections.Generic;
using System;

public class SquadKeeper
{
    private Dictionary<SquadPlan, int> _squads;

    public SquadKeeper(Dictionary<SquadPlan, int> squads) 
    {  
        _squads = squads;

        if(squads == null)
            throw new ArgumentNullException(nameof(squads));
    }

    public IReadOnlyCollection<SquadPlan> Squads => _squads.Keys;

    public int GetSquadsCount(SquadPlan squad)
    {
        if(squad == null)
            throw new ArgumentNullException(nameof(squad));

        if(_squads.TryGetValue(squad, out int squadsCount))
            return squadsCount;

        return 0;
    }

    public void AddSquad(SquadPlan squad)
    {
        if(_squads.ContainsKey(squad))
            _squads[squad]++;
        else
            _squads.Add(squad, 1);
    }
}
