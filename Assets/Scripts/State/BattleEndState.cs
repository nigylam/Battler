using Battler;
using UnityEngine;

namespace Battler.State
{
    public class BattleEndState : GameState
    {
        public BattleEndState(GameStateMachine stateMachine, GameContext context) : base(stateMachine, context) { }

        public override void Enter()
        {
            Context.BattleEndScreen.Set(Context.Rewarder.IsPlayerWin, Context.Rewarder.GoldReward, Context.Rewarder.SquadReward);
            Context.BattleEndScreen.End += OnEndClicked;
        }

        public override void Exit()
        {
            Context.BattleEndScreen.gameObject.SetActive(false);
        }

        private void OnEndClicked()
        {
            StateMachine.ChangeState(GameStateType.LevelMap);
        }
    }
}