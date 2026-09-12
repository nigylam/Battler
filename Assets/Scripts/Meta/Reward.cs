using UnityEngine;

namespace Battler.Meta
{
    public struct Reward
    {
        public bool IsPlayerWin;
        public bool IsAutoLose;
        public int GoldReward;
        public SquadGoodConfig SquadReward;
        public bool CanAddRound;

        public Reward(bool isPlayerWin, bool isAutoLose, int goldReward, SquadGoodConfig squadReward, bool canAddRound)
        {
            IsPlayerWin = isPlayerWin;
            IsAutoLose = isAutoLose;
            GoldReward = goldReward;
            SquadReward = squadReward;
            CanAddRound = canAddRound;
        }
    }
}
