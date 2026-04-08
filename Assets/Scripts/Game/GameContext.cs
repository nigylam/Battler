
public class GameContext
{
    public GameContext(SquadKeeper squadKeeper, Gold gold, MainMenu mainMenu, LevelMenu levelMenu, BattleEndScreen battleEndScreen, Battle battle)
    {
        SquadKeeper = squadKeeper;
        Gold = gold;
        MainMenu = mainMenu;
        LevelMenu = levelMenu;
        BattleEndScreen = battleEndScreen;
        Battle = battle;
    }

    public SquadKeeper SquadKeeper { get; }
    public Gold Gold { get; }
    public MainMenu MainMenu { get; }
    public LevelMenu LevelMenu { get; }
    public BattleEndScreen BattleEndScreen { get; }
    public Battle Battle { get; }

    public Level Level { get; private set; }

    public void SetLevel(Level level)
    {
        Level = level;
    }

}
