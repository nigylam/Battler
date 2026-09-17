using Battler.Core.SquadKeeping;
using System.Collections.Generic;
using UnityEngine;

namespace Battler.BattleSystem
{
    public readonly struct LevelSettings
    {
        public readonly bool IsRoundReplay;
        public readonly LevelConfig LevelConfig;

        public LevelSettings(bool isRoundReplay, LevelConfig levelConfig)
        {
            IsRoundReplay = isRoundReplay;
            LevelConfig = levelConfig;
        }
    }
}
