
public class GameContext
{
    public GameContext
    (
        SquadKeeper squadKeeper, 
        Gold gold, Shop shop, 
        MainMenu mainMenu, 
        LevelMenu levelMenu, 
        ShopMenu shopMenu, 
        BattleEndScreen battleEndScreen, 
        Battle battle
    )
    {
        SquadKeeper = squadKeeper;
        Gold = gold;
        Shop = shop;
        MainMenu = mainMenu;
        LevelMenu = levelMenu;
        ShopMenu = shopMenu;
        BattleEndScreen = battleEndScreen;
        Battle = battle;
    }

    public SquadKeeper SquadKeeper { get; }
    public Gold Gold { get; }
    public Shop Shop { get; }
    public MainMenu MainMenu { get; }
    public LevelMenu LevelMenu { get; }
    public ShopMenu ShopMenu { get; }
    public BattleEndScreen BattleEndScreen { get; }
    public Battle Battle { get; }

    public Level Level { get; private set; }

    public void SetLevel(Level level)
    {
        Level = level;
    }

}
