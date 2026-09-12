using Battler.Core;
using Battler.UI.BattleView;
using System;
using UnityEngine;

namespace Battler.State
{
    public class BattlePauseState : GameState
    {
        private readonly BattlePauseMenu _battlePauseMenu;
        private readonly Battle _battle;

        public BattlePauseState(GameStateMachine stateMachine, BattlePauseMenu battlePauseMenu, Battle battle) : base(stateMachine)
        {
            _battlePauseMenu = battlePauseMenu ?? throw new ArgumentNullException(nameof(battlePauseMenu));
            _battle = battle;
        }

        public override void Enter(GameContext context)
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
            _battle.ResumeGame();
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