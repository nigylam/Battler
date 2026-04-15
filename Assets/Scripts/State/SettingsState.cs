using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battler.State
{
    public class SettingsState : GameState
    {
        public SettingsState(GameStateMachine stateMachine, GameContext context) : base(stateMachine, context) { }

        public override void Enter()
        {
            Context.SettingsMenu.gameObject.SetActive(true);
            Context.SettingsMenu.Resume += OnResumeClick;
        }

        public override void Exit()
        {
            Context.SettingsMenu.Resume -= OnResumeClick;
            Context.SettingsMenu.gameObject.SetActive(false);
        }

        private void OnResumeClick()
        {
            StateMachine.PopState();
        }
    }
}
