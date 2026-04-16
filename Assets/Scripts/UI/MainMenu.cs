using System;
using UnityEngine;
using UnityEngine.UI;

namespace Battler.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _settingsButton;

        public event Action Start;
        public event Action Settings;

        private void OnEnable()
        {
            _startButton.onClick.AddListener(OnStartClick);
            _settingsButton.onClick.AddListener(OnSettingsClick);
        }

        private void OnDisable()
        {
            _startButton.onClick.RemoveListener(OnStartClick);
            _settingsButton.onClick.RemoveListener(OnSettingsClick);
        }

        private void OnStartClick()
        {
            _startButton.onClick.RemoveListener(OnStartClick);
            Start?.Invoke();
        }

        private void OnSettingsClick()
        {
            Settings?.Invoke();
        }
    }
}