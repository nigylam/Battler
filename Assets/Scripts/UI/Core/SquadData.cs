using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SquadData
{
    public SquadPlan Squad;
    public int Count;

    public SquadData(SquadPlan squad, int count)
    {
        Squad = squad;
        Count = count;
    }
}
