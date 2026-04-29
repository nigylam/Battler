using Battler.Meta;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Battler.UI.LevelView
{
    public class LevelMenu : MonoBehaviour
    {
        [SerializeField] private List<LevelButton> _levelButtons;
        [SerializeField] private TextCounter _goldCounter;
        [SerializeField] private UIButton _shopButton;
        [SerializeField] private UIButton _settingsButton;
        [SerializeField] private UIButton _mainMenuButton;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _buySound;
        [SerializeField] private AudioClip _cancelBuySound;

        private bool _winGameNotificationShowed;
        private LevelProgress _levelProgress;

        public event Action<LevelConfig> Start;
        public event Action Shop;
        public event Action Settings;
        public event Action MainMenu;

        public bool ShowWinGame { get; private set; }

        private void OnEnable()
        {
            foreach (LevelButton levelButton in _levelButtons)
            {
                levelButton.Clicked += OnLevelClick;
                levelButton.SetInteractable(_levelProgress.Opened(levelButton.Level));
            }

            _shopButton.Clicked += OnShopClick;
            _settingsButton.Clicked += OnSettingsClick;
            _mainMenuButton.Clicked += OnMainMenuClick;
            _goldCounter.Enable();
        }

        private void OnDisable()
        {
            foreach (LevelButton levelButton in _levelButtons)
                levelButton.Clicked -= OnLevelClick;

            _shopButton.Clicked -= OnShopClick;
            _settingsButton.Clicked -= OnSettingsClick;
            _mainMenuButton.Clicked -= OnMainMenuClick;
            _goldCounter.Disable();
        }

        public void Initialize(Gold gold, LevelProgress progress)
        {
            _goldCounter.Initialize(gold);
            _levelProgress = progress;
        }

        public void Enable(LevelProgress levelProgress)
        {
            gameObject.SetActive(true);

            if (levelProgress.AllLevelsCompleted)
            {
                ShowNotification();
            }
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
            _shopButton.Clicked -= OnShopClick;
            Shop?.Invoke();
        }

        private void OnMainMenuClick()
        {
            MainMenu?.Invoke();
        }
            
        private void ShowNotification()
        {
            if (_winGameNotificationShowed)
            {
                ShowWinGame = false;
                return;
            }

            ShowWinGame = true;
            _winGameNotificationShowed = true;
        }
    }
}