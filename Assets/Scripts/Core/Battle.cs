using Battler.Battle.Armies;
using Battler.UI.BattleView;
using System;
using UnityEngine;

namespace Battler.Core
{
    public class Battle : MonoBehaviour
    {
        [SerializeField] private PlayerSide _player;
        [SerializeField] private EnemySide _enemy;
        [SerializeField] private BattleMenu _battleMenu;
        [SerializeField] private CameraMover _cameraMover;

        private float _defaultTimeScale;
        private float _pauseTimeScale = 0;
        private bool _isBattleActive;
        private int _roundsToWin = 2;
        private int _sidesCount = 2;
        private bool _haveWinner;
        private int _sidesReadyForRound;
        private int _sidesEndRound;

        public event Action<bool> End;
        public event Action Pause;

        private void OnEnable()
        {
            _player.ReadyForRound += OnReadyForRound;
            _enemy.ReadyForRound += OnReadyForRound;
            _player.RoundEnded += OnRoundEnded;
            _enemy.RoundEnded += OnRoundEnded;
            _player.WinRound += OnPlayerWinRound;
            _enemy.WinRound += OnEnemyWinRound;
            _battleMenu.PlayerWin += OnPlayerWin;
            _battleMenu.EnemyWin += OnEnemyWin;
            _battleMenu.Pause += OnPause;
            _defaultTimeScale = Time.timeScale;
        }

        private void OnDisable()
        {
            _player.ReadyForRound -= OnReadyForRound;
            _enemy.ReadyForRound -= OnReadyForRound;
            _player.RoundEnded -= OnRoundEnded;
            _enemy.RoundEnded -= OnRoundEnded;
            _player.WinRound -= OnPlayerWinRound;
            _enemy.WinRound -= OnEnemyWinRound;
            _battleMenu.PlayerWin -= OnPlayerWin;
            _battleMenu.EnemyWin -= OnEnemyWin;
            _battleMenu.Pause -= OnPause;
        }

        public void StartLevel(GameContext context)
        {
            _haveWinner = false;
            _enemy.StartLevel(context);
            _player.StartLevel(context);
            PrepareToRound();
            _battleMenu.gameObject.SetActive(true);
            _battleMenu.Initialize(_roundsToWin);
            _cameraMover.gameObject.SetActive(true);
            Time.timeScale = _defaultTimeScale;
        }

        public void EndLevel()
        {
            _battleMenu.gameObject.SetActive(false);
            _cameraMover.gameObject.SetActive(false);
            _enemy.EndLevel();
            _player.EndLevel();
            _sidesReadyForRound = 0;
            _sidesEndRound = 0;
            _haveWinner = false;
            _isBattleActive = false;
        }

        public void ResumeGame()
        {
            Time.timeScale = _defaultTimeScale;
            _cameraMover.gameObject.SetActive(true);
        }

        private void OnPause()
        {
            Pause?.Invoke();
            PauseGame();
        }

        private void PauseGame()
        {
            if (_isBattleActive)
                Time.timeScale = _pauseTimeScale;

            _cameraMover.gameObject.SetActive(false);
        }

        private void PrepareToRound()
        {
            if (_haveWinner)
                return;

            _player.PrepareToRound();
            _enemy.PrepareToRound();
        }

        private void OnRoundEnded()
        {
            if (++_sidesEndRound == _sidesCount)
            {
                PrepareToRound();
                _sidesEndRound = 0;
            }
        }

        private void OnReadyForRound()
        {
            if (++_sidesReadyForRound == _sidesCount)
            {
                _player.StartRound();
                _enemy.StartRound();
                _sidesReadyForRound = 0;
                _isBattleActive = true;
            }
        }

        private void OnPlayerWinRound()
        {
            _battleMenu.OnPlayerWinRound();
            OnRoundEnd();
        }

        private void OnEnemyWinRound()
        {
            _battleMenu.OnEnemyWinRound();
            OnRoundEnd();
        }

        private void OnRoundEnd()
        {
            _isBattleActive = false;
            _player.EndRound();
            _enemy.EndRound();
        }

        private void OnPlayerWin()
        {
            _haveWinner = true;
            End?.Invoke(true);
        }

        private void OnEnemyWin()
        {
            _haveWinner = true;
            End?.Invoke(false);
        }
    }
}