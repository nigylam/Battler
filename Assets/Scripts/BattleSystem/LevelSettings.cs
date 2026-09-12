using Battler.Core.SquadKeeping;
using System.Collections.Generic;
using UnityEngine;

namespace Battler.BattleSystem
{
    public struct LevelSettings
    {
        public bool IsRoundReplay;
        public IReadOnlyCollection<EnemyRound> EnemyRounds;

        public LevelSettings(bool isRoundReplay, IReadOnlyCollection<EnemyRound> enemyRounds)
        {
            IsRoundReplay = isRoundReplay;
            EnemyRounds = enemyRounds;
        }
    }
}
