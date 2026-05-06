using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battler.BattleSystem.Squads
{
    public interface ISquadSpawnContext
    {
        public SquadPlan Plan { get; }
        public bool CreateUpgraded { get; }
        public (int x, int y) StartCell { get; }
    }
}
