using Battler;
using System;
using UnityEngine;

namespace Battler.State
{
    public class BattleState : GameState
    {
        public BattleState(GameStateMachine stateMachine, GameContext context) : base(stateMachine, context) { }

        public override void Enter()
        {
            Context.Battle.StartLevel(Context);
            Context.Battle.End += OnBattleEnd;
            Context.Battle.Pause += OnBattlePause;
        }

        public override void Exit()
        {
            Context.Battle.EndLevel();
            Context.Battle.End -= OnBattleEnd;
            Context.Battle.Pause -= OnBattlePause;
        }

        private void OnBattleEnd(bool isPlayerWin)
        {
            Context.Rewarder.GenerateReward(isPlayerWin, Context);
            StateMachine.PushState(GameStateType.BattleEnd);
        }

        private void OnBattlePause()
        {
            StateMachine.PushState(GameStateType.BattlePause);
        }
    }
}