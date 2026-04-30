using Battler.UI.BattleView;
using System;
using UnityEngine;

namespace Battler.State
{
    public class BattlePauseState : GameState
    {
        private readonly BattlePauseMenu _battlePauseMenu;

        public BattlePauseState(GameStateMachine stateMachine, GameContext context, BattlePauseMenu battlePauseMenu) : base(stateMachine, context)
        {
            _battlePauseMenu = battlePauseMenu ?? throw new ArgumentNullException(nameof(battlePauseMenu));
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
            StateMachine.PushState(GameStateType.QuitApprove);
        }

        private void OnSettingsClick()
        {
            StateMachine.PushState(GameStateType.Settings);
        }
    }
}