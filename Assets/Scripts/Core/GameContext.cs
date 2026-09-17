using Battler.BattleSystem;
using Battler.Meta;
using System;

namespace Battler.Core
{
    public readonly struct GameContext
    {
        public readonly LevelSettings LevelSettings;
        public readonly BattleEndContext BattleEndContext;

        public GameContext(BattleEndContext battleEndContext)
        {
            LevelSettings = new LevelSettings();
            BattleEndContext = battleEndContext;
        }

        public GameContext(LevelSettings levelSettings)
        {
            LevelSettings = levelSettings;
            BattleEndContext = new BattleEndContext();
        }

        public GameContext(LevelSettings levelSettings, BattleEndContext battleEndContext)
        {
            LevelSettings = levelSettings;
            BattleEndContext = battleEndContext;
        }
    }
}