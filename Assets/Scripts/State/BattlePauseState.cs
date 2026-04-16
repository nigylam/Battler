using Battler.UI.BattleView;
using UnityEngine;

namespace Battler.State
{
    public class BattlePauseState : GameState
    {
        private BattlePauseMenu _battlePauseMenu;

        public BattlePauseState(GameStateMachine stateMachine, GameContext context, BattlePauseMenu battlePauseMenu) : base(stateMachine, context)
        {
            _battlePauseMenu = battlePauseMenu;
        }

        public override void Enter()
        {
            _battlePauseMenu.gameObject.SetActive(true);
            _battlePauseMenu.Resume += OnResumeClick;
            _battlePauseMenu.Quit += OnQuitClick;
            _battlePauseMenu.Settings += OnSettingsClick;
        }

        public override void Exit()
        {
            _battlePauseMenu.Resume -= OnResumeClick;
            _battlePauseMenu.Quit -= OnQuitClick;
            _battlePauseMenu.Settings -= OnSettingsClick;
            _battlePauseMenu.gameObject.SetActive(false);
        }

        private void OnResumeClick()
        {
            Context.Battle.ResumeGame();
            StateMachine.PopState();
        }

        private void OnQuitClick()
        {
            StateMachine.ChangeState(GameStateType.LevelMap);
        }

        private void OnSettingsClick()
        {
            StateMachine.PushState(GameStateType.Settings);
        }
    }
}