using System;
using UnityEngine;
using UnityEngine.UI;

namespace Battler.UI.BattleView
{
    public class BattleMenu : MonoBehaviour
    {
        [SerializeField] private UIButton _pauseButton;
        [SerializeField] private RoundWinnerPannel _roundWinnerPanel;
        [SerializeField] private RoundWinsPannel _roundWinsPanel;
        [SerializeField] private UIButton _startButton;
        [SerializeField] private DragArmyPanel _armyPanel;
        [SerializeField] private Image _armyPanelImage;
        [SerializeField] private Color _armyPanelDefaultColor;
        [SerializeField] private Color _armyPanelPlacingColor;

        public event Action Pause;
        public event Action PlayerWin;
        public event Action EnemyWin;
        public event Action StartButtonClicked;

        public DragArmyPanel ArmyPannel => _armyPanel;

        private void OnEnable()
        {
            Restart();
            _roundWinsPanel.EnemyWin += OnEnemyWin;
            _roundWinsPanel.PlayerWin += OnPlayerWin;
            _pauseButton.Clicked += OnPauseClick;
        }

        private void OnDisable()
        {
            _roundWinsPanel.EnemyWin -= OnEnemyWin;
            _roundWinsPanel.PlayerWin -= OnPlayerWin;
            _pauseButton.Clicked -= OnPauseClick;
            _startButton.Clicked -= OnStartClick;
            _roundWinnerPanel.Restart();
            _startButton.gameObject.SetActive(false);
            _roundWinnerPanel.gameObject.SetActive(false);
        }

        public void SetSquads(Keeper<BattleSquadCell> keeper)
        {
            _armyPanel.SetItems(keeper);
        }

        public void EnablePlayButton()
        {
            if (_startButton.gameObject.activeSelf)
                return;

            _startButton.gameObject.SetActive(true);
            _startButton.Clicked += OnStartClick;
        }

        public void DisablePlayButton()
        {
            if (_startButton.gameObject.activeSelf == false)
                return;

            _startButton.Clicked -= OnStartClick;
            _startButton.gameObject.SetActive(false);
        }

        public void SetPlacingAvailable()
        {
            _armyPanelImage.color = _armyPanelPlacingColor;
        }

        public void SetPlacingUnavailable()
        {
            _armyPanelImage.color = _armyPanelDefaultColor;
        }

        public void Initialize(int roundsToWin)
        {
            _roundWinsPanel.Initialize(roundsToWin);
        }

        public void OnPlayerWinRound()
        {
            _roundWinsPanel.PlayerIncrease();
        }

        public void OnEnemyWinRound()
        {
            _roundWinsPanel.EnemyIncrease();
        }

        public void SetEnemyWinPanel()
        {
            _roundWinnerPanel.SetEnemyWinner();
        }

        public void SetPlayerWinPanel()
        {
            _roundWinnerPanel.SetPlayerWinner();
        }

        private void Restart()
        {
            _roundWinsPanel.Restart();
        }

        private void OnStartClick()
        {
            StartButtonClicked?.Invoke();
            _startButton.Clicked -= OnStartClick;
            _startButton.gameObject.SetActive(false);   
        }

        private void OnPauseClick()
        {
            Pause?.Invoke();
        }

        private void OnEnemyWin()
        {
            EnemyWin?.Invoke();
        }

        private void OnPlayerWin()
        {
            PlayerWin?.Invoke();
        }
    }
}