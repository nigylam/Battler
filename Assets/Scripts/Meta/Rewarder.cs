using Battler.BattleSystem;
using System;
using UnityEngine;

namespace Battler.Meta
{
    public class Rewarder
    {
        private const int AdditionalGoldReward = 20;
        private const int MinWinRoundsForAdditionalRound = 1;

        private readonly Score _score;
        private readonly Gold _gold;
        private readonly Shop _shop;
        private readonly LevelProgress _levelProgress;

        public Rewarder(Score score, Gold gold, Shop shop, LevelProgress levelProgress)
        {
            _score = score;
            _gold = gold;
            _shop = shop;
            _levelProgress = levelProgress;
        }

        public Reward GenerateReward(BattleEndContext battleEndContext, LevelSettings levelSettings, LevelConfig level)
        {
            Reward reward;

            if (battleEndContext.IsPlayerWin == false)
            {
                bool canAddRound = 
                    battleEndContext.IsAutoLose == false 
                    && battleEndContext.PlayerWinRounds >= MinWinRoundsForAdditionalRound
                    && levelSettings.IsRoundReplay == false;
                reward = new Reward(false, battleEndContext.IsAutoLose, 0, null, canAddRound);
                return reward;
            }

            _score.Increase(level.ScoreReward);
            _gold.Increase(level.GoldReward);
            int goldReward = level.GoldReward;
            SquadGoodConfig squadReward = null;

            if (_levelProgress.Completed(level) == false)
            {
                _levelProgress.SetCompleted(level);

                if (level.SquadReward != null)
                {
                    squadReward = level.SquadReward;
                    _shop.Unlock(squadReward);
                }
            }

            reward = new Reward(true, false, goldReward, squadReward, false);
            return reward;
        }

        public void AddAdditionalReward()
        {
            _gold.Increase(AdditionalGoldReward);
        }
    }
}