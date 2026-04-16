using Battler.Core;
using Battler.UI;
using Battler.UI.BattleView;
using Battler.UI.LevelView;

public class GameContext
{
    public GameContext
    (
        SquadKeeper squadKeeper,
        Gold gold,
        Shop shop,
        LevelProgress levelProgress,
        MainMenu mainMenu,
        LevelMenu levelMenu,
        ShopMenu shopMenu,
        BattleEndScreen battleEndScreen,
        BattlePauseMenu battlePauseMenu,
        SettingsMenu settingsMenu,
        Battle battle
    )
    {
        SquadKeeper = squadKeeper;
        Gold = gold;
        Shop = shop;
        LevelProgress = levelProgress;
        MainMenu = mainMenu;
        LevelMenu = levelMenu;
        ShopMenu = shopMenu;
        BattleEndScreen = battleEndScreen;
        BattlePauseMenu = battlePauseMenu;
        SettingsMenu = settingsMenu;
        Battle = battle;
        Rewarder = new Rewarder();
    }

    public SquadKeeper SquadKeeper { get; }
    public Gold Gold { get; }
    public Shop Shop { get; }
    public LevelProgress LevelProgress { get; }
    public MainMenu MainMenu { get; }
    public LevelMenu LevelMenu { get; }
    public ShopMenu ShopMenu { get; }
    public BattleEndScreen BattleEndScreen { get; }
    public BattlePauseMenu BattlePauseMenu { get; }
    public SettingsMenu SettingsMenu { get; }
    public Battle Battle { get; }
    public Rewarder Rewarder { get; }

    public LevelConfig Level { get; private set; }

    public void SetLevel(LevelConfig level)
    {
        Level = level;
    }

}
