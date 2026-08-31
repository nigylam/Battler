using System;
using UnityEngine;

namespace Battler.Meta
{
    public class Rewarder
    {
        private const int AdditionalGoldReward = 20;

        public Rewarder() { }

        public bool IsPlayerWin { get; private set; }
        public bool IsAutoLose { get; private set; }
        public int GoldReward { get; private set; }
        public SquadGoodConfig SquadReward { get; private set; }

        public void GenerateReward(bool isPlayerWin, bool isAutoLose, GameContext context)
        {
            SquadReward = null;
            LevelConfig level = context.Level;
            IsPlayerWin = isPlayerWin;
            IsAutoLose = isAutoLose;

            if (IsPlayerWin == false)
                return;

            context.Score.Increase(context.Level.ScoreReward);
            context.Gold.Increase(context.Level.GoldReward);
            GoldReward = context.Level.GoldReward;

            if (context.LevelProgress.Completed(level) == false)
            {
                context.LevelProgress.SetCompleted(level);

                if (context.Level.SquadReward != null)
                {
                    SquadReward = context.Level.SquadReward;
                    context.Shop.Unlock(SquadReward);
                }
            }
        }

        public void GenerateAdditionalReward(GameContext context)
        {
            context.Gold.Increase(AdditionalGoldReward);
        }
    }
}