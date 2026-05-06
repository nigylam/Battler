using UnityEngine;

namespace Battler.BattleSystem.Squads
{
    public class SquadContext
    {
        public SquadContext(SquadPlan plan, bool createUpgraded = false)
        {
            Plan = plan;
            CreateUpgraded = createUpgraded;
        }

        public SquadPlan Plan { get; }
        public bool CreateUpgraded { get; private set; }

        public void Upgrade()
        {
            CreateUpgraded = true;
        }
    }
}
