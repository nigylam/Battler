using System;
using UnityEngine;

namespace Battler.UI.BattleView
{
    public class BattlePauseMenu : PopupMenu
    {
        [SerializeField] private UIButton _settingsButton;
        [SerializeField] private UIButton _quitButton;

        public event Action Settings;
        public event Action Quit;

        protected override void Enable()
        {
            _quitButton.Clicked += OnQuitClick;
            _settingsButton.Clicked += OnSettingsClick;
        }

        protected override void Disable()
        {
            _quitButton.Clicked -= OnQuitClick;
            _settingsButton.Clicked -= OnSettingsClick;
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

