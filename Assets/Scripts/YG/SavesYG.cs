using Battler;
using Battler.Core.SquadKeeping;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YG
{
    public partial class SavesYG
    {
        public int gold = 100;
        public List<LevelConfig> openedLevels = new();
        public List<SquadGoodConfig> openedGoods = new();
        public List<SquadPlan> boughtSquads = new();
    }
}
