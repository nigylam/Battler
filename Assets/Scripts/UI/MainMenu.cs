using System;
using UnityEngine;

namespace Battler.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private UIButton _startButton;
        [SerializeField] private UIButton _settingsButton;
        [SerializeField] private UIButton _leaderboardButton;

        public event Action Start;
        public event Action Settings;
        public event Action Leaderboard;

        private void OnEnable()
        {
            _startButton.Clicked += OnStartClick;
            _settingsButton.Clicked += OnSettingsClick;
            _leaderboardButton.Clicked += OnLeaderboardClick;
        }

        private void OnDisable()
        {
            _startButton.Clicked -= OnStartClick;
            _settingsButton.Clicked -= OnSettingsClick;
            _leaderboardButton.Clicked -= OnLeaderboardClick;
        }

        private void OnStartClick()
        {
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