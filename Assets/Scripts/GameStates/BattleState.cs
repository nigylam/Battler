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
            Context.Battle.AutoLose += OnAutoLose;
        }

        public override void Exit()
        {
            Context.Battle.CloseLevel();
            Context.Battle.End -= OnBattleEnd;
            Context.Battle.Pause -= OnBattlePause;
            Context.Battle.AutoLose -= OnAutoLose;

        }

        private void OnBattleEnd(bool isPlayerWin)
        {
            Context.Rewarder.GenerateReward(isPlayerWin, false, Context);
            StateMachine.PushState(GameStateType.BattleEnd);
        }

        private void OnAutoLose()
        {
            Context.Rewarder.GenerateReward(false, true, Context);
            StateMachine.PushState(GameStateType.BattleEnd);
        }

        private void OnBattlePause()
        {
            StateMachine.PushState(GameStateType.BattlePause);
        }
    }
}