using System;
using UnityEngine;
using UnityEngine.UI;

namespace Battler.UI.BattleView
{
    public class BattleMenu : MonoBehaviour
    {
        [SerializeField] private UIButton _pauseButton;
        [SerializeField] private TextCounter _roundTextCounter;
        [SerializeField] private RoundWinnerPannel _roundWinnerPannel;
        [SerializeField] private RoundWinsPannel _roundWinsPannel;
        [SerializeField] private UIButton _startButton;
        [SerializeField] private DragArmyPanel _armyPanel;
        [SerializeField] private Image _armyPanelImage;
        [SerializeField] private Color _armyPanelDefaultColor;
        [SerializeField] private Color _armyPanelPlacingColor;

        private RoundCounter _roundCounter;
        private bool _haveWinner;

        public event Action Pause;
        public event Action PlayerWin;
        public event Action EnemyWin;
        public event Action StartButtonClicked;

        public DragArmyPanel ArmyPannel => _armyPanel;

        private void Awake()
        {
            _roundCounter = new RoundCounter();
        }

        private void OnEnable()
        {
            _roundTextCounter.Initialize(_roundCounter);
            _roundTextCounter.Enable();
            Restart();
            _roundCounter.Increase();
            _roundWinsPannel.EnemyWin += OnEnemyWin;
            _roundWinsPannel.PlayerWin += OnPlayerWin;
            _pauseButton.Clicked += OnPauseClick;
        }

        private void OnDisable()
        {
            _roundTextCounter.Disable();
            _roundWinsPannel.EnemyWin -= OnEnemyWin;
            _roundWinsPannel.PlayerWin -= OnPlayerWin;
            _pauseButton.Clicked += OnPauseClick;
            _startButton.Clicked -= OnStartClick;
            _startButton.gameObject.SetActive(false);
            _roundWinnerPannel.gameObject.SetActive(false);
        }

        public void SetSquads(Keeper<BattleSquadCell> keeper)
        {
            _armyPanel.SetItems(keeper);
        }

        public void SetPlayButtonActive()
        {
            if (_startButton.gameObject.activeSelf)
                return;

            _startButton.gameObject.SetActive(true);
            _startButton.Clicked += OnStartClick;
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
            _roundWinsPannel.Initialize(roundsToWin);
        }

        public void OnPlayerWinRound()
        {
            _roundWinnerPannel.SetPlayerWinner();
            _roundWinsPannel.PlayerIncrease();

            if (_haveWinner == false)
                _roundCounter.Increase();
        }

        public void OnEnemyWinRound()
        {
            _roundWinnerPannel.SetEnemyWinner();
            _roundWinsPannel.EnemyIncrease();

            if (_haveWinner == false)
                _roundCounter.Increase();
        }

        private void Restart()
        {
            _roundCounter.Restart();
            _roundWinsPannel.Restart();
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
            _haveWinner = true;
            EnemyWin?.Invoke();
        }

        private void OnPlayerWin()
        {
            _haveWinner = true;
            PlayerWin?.Invoke();
        }
    }
}