using System.Collections.Generic;
using UnityEngine;

namespace YG
{
    public partial class SavesYG
    {
        public int gold = 100;
        public int score = 0;
        public List<LevelConfig> openedLevels = new();
        public List<SquadGoodConfig> openedGoods = new();
        public List<SquadPlan> boughtSquads = new();
        public float soundUI = 1f;
        public float soundSFX = 1f;
    }
}
