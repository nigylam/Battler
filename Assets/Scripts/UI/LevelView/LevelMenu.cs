using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Battler.UI.LevelView
{
    public class LevelMenu : MonoBehaviour
    {
        [SerializeField] private List<LevelButton> _levelButtons;
        [SerializeField] private TextCounter _goldCounter;
        [SerializeField] private Button _shopButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _mainMenuButton;

        public event Action<LevelConfig> Start;
        public event Action Shop;
        public event Action Settings;
        public event Action MainMenu;

        private void OnEnable()
        {
            foreach (LevelButton levelButton in _levelButtons)
                levelButton.Clicked += OnLevelClick;

            _shopButton.onClick.AddListener(OnShopClick);
            _settingsButton.onClick.AddListener(OnSettingsClick);
            _mainMenuButton.onClick.AddListener(OnMainMenuClick);
        }

        private void OnDisable()
        {
            foreach (LevelButton levelButton in _levelButtons)
                levelButton.Clicked -= OnLevelClick;

            _shopButton.onClick.RemoveListener(OnShopClick);
            _settingsButton.onClick.RemoveListener(OnSettingsClick);
            _mainMenuButton.onClick.RemoveListener(OnMainMenuClick);
        }

        public void Initialize(Gold gold)
        {
            _goldCounter.Initialize(gold);
        }

        private void OnSettingsClick()
        {
            Settings?.Invoke();
        }

        private void OnLevelClick(LevelConfig level)
        {
            Start?.Invoke(level);
        }

        private void OnShopClick()
        {
            _shopButton.onClick.RemoveListener(OnShopClick);
            Shop?.Invoke();
        }

        private void OnMainMenuClick()
        {
            MainMenu?.Invoke();
        }
    }
}