using System;
using UnityEngine;
using UnityEngine.UI;

namespace Battler.UI.BattleView
{
    public class BattlePauseMenu : PopupMenu
    {
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _quitButton;

        public event Action Settings;
        public event Action Quit;

        protected override void Enable()
        {
            _quitButton.onClick.AddListener(OnQuitClick);
            _settingsButton.onClick.AddListener(OnSettingsClick);
        }

        protected override void Disable()
        {
            _quitButton.onClick.RemoveListener(OnQuitClick);
            _settingsButton.onClick.RemoveListener(OnSettingsClick);
        }

        private void OnQuitClick()
        {
            Quit?.Invoke();
        }

        private void OnSettingsClick()
        {
            Settings?.Invoke();
        }
    }
}

