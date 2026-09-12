using Battler.BattleSystem;
using Battler.UI.BattleView;
using System;
using UnityEngine;

public class RoundWinsPannel : MonoBehaviour
{
    [SerializeField] private WinsCounter _enemyWinsCounter;
    [SerializeField] private IconCounter _enemyWinsIcons;
    [SerializeField] private WinsCounter _playerWinsCounter;
    [SerializeField] private IconCounter _playerWinsIcons;
    [SerializeField] private Canvas _canvas;

    public event Action<RoundsCount> WinConditionAchieved;

    private void OnEnable()
    {
        _enemyWinsCounter.Win += OnWinConditionAchieved;
        _playerWinsCounter.Win += OnWinConditionAchieved;
    }

    private void OnDisable()
    {
        _enemyWinsCounter.Win -= OnWinConditionAchieved;
        _playerWinsCounter.Win -= OnWinConditionAchieved;
    }

    public void Initialize(int roundsToWin)
    {
        _enemyWinsCounter.Initialize(roundsToWin);
        _enemyWinsIcons.Initialize(_enemyWinsCounter);
        _playerWinsCounter.Initialize(roundsToWin);
        _playerWinsIcons.Initialize(_playerWinsCounter);

    }

    public void PlayerIncrease()
    {
        _playerWinsCounter.Increase();
    }

    public void EnemyIncrease()
    {
        _enemyWinsCounter.Increase();
    }

    public void Restart()
    {
        _enemyWinsCounter.Restart();
        _playerWinsCounter.Restart();
    }

    private void OnWinConditionAchieved()
    {
        WinConditionAchieved?.Invoke(new RoundsCount((int)_playerWinsCounter.Current, (int)_enemyWinsCounter.Current));
    }
}
