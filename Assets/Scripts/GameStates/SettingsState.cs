using Battler.Core;
using Battler.UI;
using UnityEngine;

namespace Battler.State
{
    public class SettingsState : GameState
    {
        private readonly SettingsMenu _settingsMenu;

        public SettingsState(GameStateMachine stateMachine, SettingsMenu settingsMenu) : base(stateMachine)
        {
            _settingsMenu = settingsMenu;
        }

        public override void Enter(GameContext context)
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
