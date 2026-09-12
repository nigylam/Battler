using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battler.BattleSystem
{
    public struct RoundsCount
    {
        public int PlayerWins;
        public int EnemyWins;

        public RoundsCount(int playerWins, int enemyWins)
        {
            PlayerWins = playerWins;
            EnemyWins = enemyWins;
        }
    }
}
