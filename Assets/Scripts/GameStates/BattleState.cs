using Battler.BattleSystem;
using Battler.Core;
using Battler.Core.SquadKeeping;
using System;
using UnityEngine;

namespace Battler.State
{
    public class BattleState : GameState
    {
        private readonly Battle _battle;
        private readonly GameSquadKeeper _gameSquadKeeper;

        private LevelSettings _levelSettings;

        public BattleState(GameStateMachine stateMachine, Battle battle, GameSquadKeeper gameSquadKeeper) : base(stateMachine)
        {
            _battle = battle;
            _gameSquadKeeper = gameSquadKeeper;
        }

        public override void Enter(GameContext context)
        {
            _levelSettings = context.LevelSettings;
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
            var context = new GameContext(_levelSettings, battleEndContext);
            StateMachine.PushState(GameStateType.BattleEnd, context);
        }

        private void OnBattlePause()
        {
            StateMachine.PushState(GameStateType.BattlePause);
        }
    }
}