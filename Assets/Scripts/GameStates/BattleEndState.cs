using Battler.UI.BattleView;

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
            _battleEndScreen.Set(Context.Rewarder.IsPlayerWin, Context.Rewarder.IsAutoLose, Context.Rewarder.GoldReward, Context.Rewarder.SquadReward);
            _battleEndScreen.End += OnEndClicked;
            _battleEndScreen.Reward += OnRewardClicked;
        }

        public override void Exit()
        {
            _battleEndScreen.End -= OnEndClicked;
            _battleEndScreen.Reward -= OnRewardClicked;
            _battleEndScreen.gameObject.SetActive(false);
        }

        private void OnRewardClicked()
        {
            Context.Rewarder.GenerateAdditionalReward(Context);
            StateMachine.ChangeState(GameStateType.LevelMap);
        }

        private void OnEndClicked()
        {
            StateMachine.ChangeState(GameStateType.LevelMap);
        }
    }
}