using Battler.BattleSystem;
using Battler.Meta;
using System;

public struct GameContext
{
    public LevelSettings LevelSettings;
    public BattleEndContext BattleEndContext;
    public Reward Reward;
    public LevelConfig LevelConfig;

    public GameContext(BattleEndContext battleEndContext)
    {
        LevelSettings = new LevelSettings();
        BattleEndContext = battleEndContext;
        Reward = new Reward();
        LevelConfig = null;
    }

    public GameContext(BattleEndContext battleEndContext, LevelConfig levelConfig)
    {
        LevelSettings = new LevelSettings();
        BattleEndContext = battleEndContext;
        Reward = new Reward();
        LevelConfig = levelConfig;
    }

    public GameContext(LevelSettings levelSettings, LevelConfig levelConfig)
    {
        LevelSettings = levelSettings;
        BattleEndContext = new BattleEndContext();
        Reward = new Reward();
        LevelConfig = levelConfig;
    }

    public GameContext(LevelConfig levelConfig)
    {
        LevelSettings = new LevelSettings();
        BattleEndContext = new BattleEndContext();
        Reward = new Reward();
        LevelConfig = levelConfig;
    }

    public GameContext(LevelSettings levelSettings)
    {
        LevelSettings = levelSettings;
        BattleEndContext = new BattleEndContext();
        Reward = new Reward();
        LevelConfig = null;
    }

    public GameContext(LevelSettings levelSettings, BattleEndContext battleEndContext, Reward reward, LevelConfig levelConfig)
    {
        LevelSettings = levelSettings;
        BattleEndContext = battleEndContext;
        Reward = reward;
        LevelConfig = levelConfig;
    }
}