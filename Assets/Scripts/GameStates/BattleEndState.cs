using Battler.BattleSystem;
using Battler.Meta;
using Battler.UI.BattleView;
using System;

namespace Battler.State
{
    public class BattleEndState : GameState
    {
        private readonly BattleEndScreen _battleEndScreen;
        private readonly Rewarder _rewarder;

        private LevelConfig _levelConfig;

        public BattleEndState(GameStateMachine stateMachine, BattleEndScreen battleEndScreen, Rewarder rewarder) : base(stateMachine)
        {
            _battleEndScreen = battleEndScreen;
            _rewarder = rewarder;
        }

        public override void Enter(GameContext context)
        {
            _levelConfig = context.LevelConfig;
            Reward reward = _rewarder.GenerateReward(context.BattleEndContext, context.LevelSettings, context.LevelConfig);
            _battleEndScreen.Set(reward);
            _battleEndScreen.End += OnEndClicked;
            _battleEndScreen.Reward += OnRewardClicked;
            _battleEndScreen.AddRound += OnAddRoundClicked;
        }

        public override void Exit()
        {
            _battleEndScreen.End -= OnEndClicked;
            _battleEndScreen.Reward -= OnRewardClicked;
            _battleEndScreen.AddRound -= OnAddRoundClicked;
            _battleEndScreen.gameObject.SetActive(false);
        }

        private void OnAddRoundClicked()
        {
            var levelSettings = new LevelSettings(true, _levelConfig.Rounds);
            StateMachine.ChangeState(GameStateType.Battle, new GameContext(levelSettings, _levelConfig));
        }

        private void OnRewardClicked()
        {
            _rewarder.AddAdditionalReward();
            StateMachine.ChangeState(GameStateType.LevelMap);
        }

        private void OnEndClicked()
        {
            StateMachine.ChangeState(GameStateType.LevelMap);
        }
    }
}