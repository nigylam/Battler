using Battler.UI;
using UnityEngine;

namespace Battler.State
{
    public class SettingsState : GameState
    {
        private SettingsMenu _settingsMenu;

        public SettingsState(GameStateMachine stateMachine, GameContext context, SettingsMenu settingsMenu) : base(stateMachine, context)
        {
            _settingsMenu = settingsMenu;
        }

        public override void Enter()
        {
            _settingsMenu.gameObject.SetActive(true);
            _settingsMenu.Resume += OnResumeClick;
        }

        public override void Exit()
        {
            _settingsMenu.Resume -= OnResumeClick;
            _settingsMenu.gameObject.SetActive(false);
        }

        private void OnResumeClick()
        {
            StateMachine.PopState();
        }
    }
}
