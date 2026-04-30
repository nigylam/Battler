using Battler.UI;
using UnityEngine;
using System;

namespace Battler.State
{
    public class QuitApproveState : GameState
    {
        private ApprovePopup _popup;

        public QuitApproveState(GameStateMachine stateMachine, GameContext context, ApprovePopup popup) : base(stateMachine, context)
        {
            _popup = popup ?? throw new ArgumentNullException(nameof(popup));
        }

        public override void Enter()
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
