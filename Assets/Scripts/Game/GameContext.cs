using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct GameContext
{
    public GameContext(MainMenu mainMenu, LevelMenu levelMenu, BattleEndScreen battleEndScreen, Battle battle)
    {
        MainMenu = mainMenu;
        LevelMenu = levelMenu;
        BattleEndScreen = battleEndScreen;
        Battle = battle;
    }

    public MainMenu MainMenu { get; private set; }
    public LevelMenu LevelMenu { get; private set; }
    public BattleEndScreen BattleEndScreen { get; private set; }
    public Battle Battle { get; private set; }
}
