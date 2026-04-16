using UnityEngine;

public class Rewarder
{
    public Rewarder() { }

    public bool IsPlayerWin { get; private set; }
    public int GoldReward { get; private set; }
    public SquadGoodConfig SquadReward { get; private set; }

    public void GenerateReward(bool isPlayerWin, GameContext context)
    {
        SquadReward = null;
        LevelConfig level = context.Level;
        IsPlayerWin = isPlayerWin;

        if (IsPlayerWin == false)
            return;

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
}
