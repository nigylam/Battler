using Battler.BattleSystem;
using Battler.BattleSystem.Armies;
using Battler.Core.Phases;
using Battler.UI.BattleView;
using System;
using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;
using Battler.Core.SquadKeeping;

namespace Battler.Core
{
    public class Battle : MonoBehaviour
    {
        private const int RoundsToWin = 2;
        private const float PauseTimeScale = 0;

        [SerializeField] private PlayerSide _player;
        [SerializeField] private EnemySide _enemy;
        [SerializeField] private BattleMenu _menu;
        [SerializeField] private CameraMover _cameraMover;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _winRoundSound;
        [SerializeField] private AudioClip _loseRoundSound;
        [SerializeField] private AudioClip _winLevelSound;
        [SerializeField] private AudioClip _loseLevelSound;

        private CancellationTokenSource _cancelTokenSource;
        private Side _levelWinner;

        public PlayerSide Player => _player;
        public EnemySide Enemy => _enemy;
        public BattleMenu Menu => _menu;
        public CancellationToken LevelToken => _cancelTokenSource?.Token ?? this.GetCancellationTokenOnDestroy();
        public BattleSound Sound { get; private set; }
        public bool HaveLevelWinner { get; private set; }
        public Side RoundWinner { get; private set; }

        private float _defaultTimeScale;
        private bool _isBattleActive;
        private bool _isAutoLose;
        private RoundsCount _roundsCount;

        public event Action<BattleEndContext> End;
        public event Action Pause;

        private void Awake()
        {
            Sound = new BattleSound(_audioSource, _winRoundSound, _loseRoundSound, _winLevelSound, _loseLevelSound);
        }

        private void OnEnable()
        {
            Subscribe();
            _defaultTimeScale = Time.timeScale;
        }

        private void OnDisable()
        {
            Unsubscribe();
        }

        private void OnDestroy()
        {
            if (_cancelTokenSource != null)
            {
                _cancelTokenSource.Cancel();
                _cancelTokenSource.Dispose();
            }
        }

        public async void StartLevel(LevelSettings levelSettings, GameSquadKeeper squadKeeper)
        {
            SetupNewLevel(levelSettings, squadKeeper);

            while (HaveLevelWinner == false)
            {
                RoundWinner = null;
                if (await new PreparationPhase().ExecuteAsync(this).SuppressCancellationThrow())
                    return;

                _isBattleActive = true;
                var combatPhase = new CombatPhase();

                if (await combatPhase.ExecuteAsync(this).SuppressCancellationThrow())
                    return;

                RoundWinner = combatPhase.RoundWinner;
                _isBattleActive = false;

                if (await new RoundEndPhase().ExecuteAsync(this).SuppressCancellationThrow())
                    return;
            }

            EndLevel();
        }

        private void SetupNewLevel(LevelSettings levelSettings, GameSquadKeeper squadKeeper)
        {
            _roundsCount = new RoundsCount (0,0);
            RefreshCancelToken();
            HaveLevelWinner = false;
            _enemy.StartLevel(levelSettings.EnemyRounds, levelSettings.IsRoundReplay, LevelToken);
            _player.StartLevel(squadKeeper, LevelToken);
            _menu.gameObject.SetActive(true);
            _menu.Initialize(RoundsToWin);
            _cameraMover.gameObject.SetActive(true);
            Time.timeScale = _defaultTimeScale;

            if (levelSettings.IsRoundReplay)
            {
                _menu.OnEnemyWinRound();
                _menu.OnPlayerWinRound();
            }
        }

        public void CloseLevel()
        {
            _cancelTokenSource?.Cancel();
            Enemy.EndLevel();
            Player.EndLevel();
            _menu.gameObject.SetActive(false);
            _cameraMover.gameObject.SetActive(false);
        }

        public void ResumeGame()
        {
            Time.timeScale = _defaultTimeScale;
            _cameraMover.gameObject.SetActive(true);
        }

        private void RefreshCancelToken()
        {
            if (_cancelTokenSource != null)
            {
                _cancelTokenSource.Cancel();
                _cancelTokenSource.Dispose();
            }

            _cancelTokenSource = new CancellationTokenSource();
        }

        private void EndLevel()
        {
            bool isPlayerWin = _levelWinner == _player;
            var battleEndContext = new BattleEndContext(isPlayerWin, _isAutoLose, _roundsCount.PlayerWins, _roundsCount.EnemyWins);

            if (_levelWinner == _player)
                Sound.PlayWinLevelSound();
            else
                Sound.PlayLoseLevelSound();

            End?.Invoke(battleEndContext);
        }

        private void PauseGame()
        {
            if (_isBattleActive)
                Time.timeScale = PauseTimeScale;

            _cameraMover.gameObject.SetActive(false);
        }

        private void OnPause()
        {
            Pause?.Invoke();
            PauseGame();
        }

        private void OnWinCondition(RoundsCount roundsCount)
        {
            if (roundsCount.PlayerWins == RoundsToWin)
                _levelWinner = _player;
            else
                _levelWinner = _enemy;

            _roundsCount = roundsCount;
            HaveLevelWinner = true;
            _isAutoLose = false;
        }

        private void OnAutoLose()
        {
            HaveLevelWinner = true;
            _levelWinner = _enemy;
            _isAutoLose = true;
            EndLevel();
        }

        private void Subscribe()
        {
            _menu.RoundWinsPannel.WinConditionAchieved += OnWinCondition;
            _menu.Pause += OnPause;
            _player.SquadsEnded += OnAutoLose;
        }

        private void Unsubscribe()
        {
            _menu.RoundWinsPannel.WinConditionAchieved -= OnWinCondition;
            _menu.Pause -= OnPause;
            _player.SquadsEnded -= OnAutoLose;
        }
    }
}