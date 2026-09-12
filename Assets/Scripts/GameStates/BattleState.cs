using System;
using Battler.BattleSystem;
using Battler.Core;
using Battler.Core.SquadKeeping;
using UnityEngine;

namespace Battler.State
{
    public class BattleState : GameState
    {
        private readonly Battle _battle;
        private readonly GameSquadKeeper _gameSquadKeeper;

        private LevelConfig _levelConfig;

        public BattleState(GameStateMachine stateMachine, Battle battle, GameSquadKeeper gameSquadKeeper) : base(stateMachine)
        {
            _battle = battle;
            _gameSquadKeeper = gameSquadKeeper;
        }

        public override void Enter(GameContext context)
        {
            _levelConfig = context.LevelConfig;
            _battle.StartLevel(context.LevelSettings, _gameSquadKeeper);
            _battle.End += OnBattleEnd;
            _battle.Pause += OnBattlePause;
        }

        public override void Exit()
        {
            _battle.CloseLevel();
            _battle.End -= OnBattleEnd;
            _battle.Pause -= OnBattlePause;
        }

        private void OnBattleEnd(BattleEndContext battleEndContext)
        {
            var context = new GameContext(battleEndContext, _levelConfig);
            StateMachine.PushState(GameStateType.BattleEnd, context);
        }

        private void OnBattlePause()
        {
            StateMachine.PushState(GameStateType.BattlePause);
        }
    }
}