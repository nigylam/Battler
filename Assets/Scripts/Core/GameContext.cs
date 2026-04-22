using Battler.Core;
using Battler.Core.SquadKeeping;
using Battler.Meta;

public class GameContext
{
    public GameContext
    (
        GameSquadKeeper squadKeeper,
        Gold gold,
        Shop shop,
        LevelProgress levelProgress,
        Battle battle
    )
    {
        SquadKeeper = squadKeeper;
        Gold = gold;
        Shop = shop;
        LevelProgress = levelProgress;
        Battle = battle;
        Rewarder = new Rewarder();
    }

    public GameSquadKeeper SquadKeeper { get; }
    public Gold Gold { get; }
    public Shop Shop { get; }
    public LevelProgress LevelProgress { get; }
    public Battle Battle { get; }
    public Rewarder Rewarder { get; }
    public LevelConfig Level { get; private set; }

    public void SetLevel(LevelConfig level)
        => Level = level;
}