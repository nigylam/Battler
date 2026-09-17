using Battler.UI;
using UnityEngine;
using System;
using Battler.Core;

namespace Battler.State
{
    public class QuitApproveState : GameState
    {
        private ApprovePopup _popup;

        public QuitApproveState(GameStateMachine stateMachine, ApprovePopup popup) : base(stateMachine)
        {
            _popup = popup ?? throw new ArgumentNullException(nameof(popup));
        }

        public override void Enter(GameContext context)
        {
            _popup.gameObject.SetActive(true);
            _popup.Resume += OnResumeClick;
            _popup.Quit += OnQuitClick;
        }

        public override void Exit()
        {
            _popup.Resume -= OnResumeClick;
            _popup.Quit -= OnQuitClick;
            _popup.gameObject.SetActive(false);
        }

        private void OnResumeClick()
        {
            StateMachine.PopState();
        }

        private void OnQuitClick()
        {
            StateMachine.ChangeState(GameStateType.LevelMap);
        }
    }
}
