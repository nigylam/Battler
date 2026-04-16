using UnityEngine;

namespace Battler.State
{
    public class BattleEndState : GameState
    {
        private BattleEndScreen _battleEndScreen;

        public BattleEndState(GameStateMachine stateMachine, GameContext context, BattleEndScreen battleEndScreen) : base(stateMachine, context)
        {
            _battleEndScreen = battleEndScreen;
        }

        public override void Enter()
        {
            _battleEndScreen.Set(Context.Rewarder.IsPlayerWin, Context.Rewarder.GoldReward, Context.Rewarder.SquadReward);
            _battleEndScreen.End += OnEndClicked;
        }

        public override void Exit()
        {
            _battleEndScreen.End -= OnEndClicked;
            _battleEndScreen.gameObject.SetActive(false);
        }

        private void OnEndClicked()
        {
            StateMachine.ChangeState(GameStateType.LevelMap);
        }
    }
}