using System;
using UnityEngine;

namespace Battler
{
    public class BattleSquadCell : SquadCell
    {
        public BattleSquadCell(SquadPlan plan, int count, bool createUpgraded = false) : base (plan, count)
        {
            CreateUpgraded = createUpgraded;
        }

        public bool CreateUpgraded { get; private set; }
    }
}
