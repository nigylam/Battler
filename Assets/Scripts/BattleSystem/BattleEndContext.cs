using UnityEngine;

namespace Battler.BattleSystem
{
    public struct BattleEndContext
    {
        public bool IsPlayerWin;
        public bool IsAutoLose;
        public int PlayerWinRounds;
        public int PlayerLoseRounds;

        public BattleEndContext(bool isPlayerWin, bool isAutoLose, int playerWinRounds, int playerLoseRounds)
        {
            IsPlayerWin = isPlayerWin;
            IsAutoLose = isAutoLose;
            PlayerWinRounds = playerWinRounds;
            PlayerLoseRounds = playerLoseRounds;
        }
    }
}
