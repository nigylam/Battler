using System;
using UnityEngine;
using UnityEngine.UI;

namespace Battler.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _leaderboardButton;

        public event Action Start;
        public event Action Settings;
        public event Action Leaderboard;

        private void OnEnable()
        {
            _startButton.onClick.AddListener(OnStartClick);
            _settingsButton.onClick.AddListener(OnSettingsClick);
            _leaderboardButton.onClick.AddListener(OnLeaderboardClick);
        }

        private void OnDisable()
        {
            _startButton.onClick.RemoveListener(OnStartClick);
            _settingsButton.onClick.RemoveListener(OnSettingsClick);
            _leaderboardButton.onClick.RemoveListener(OnLeaderboardClick);
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

        private void OnLeaderboardClick()
        {
            Leaderboard?.Invoke();
        }
    }
}