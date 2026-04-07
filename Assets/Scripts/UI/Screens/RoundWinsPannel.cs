using System;
using UnityEngine;

public class RoundWinsPannel : MonoBehaviour
{
    [SerializeField] private WinsCounter _enemyWinsCounter;
    [SerializeField] private IconCounter _enemyWinsIcons;
    [SerializeField] private WinsCounter _playerWinsCounter;
    [SerializeField] private IconCounter _playerWinsIcons;

    public event Action PlayerWin;
    public event Action EnemyWin;

    private void OnEnable()
    {
        _enemyWinsCounter.Win += OnEnemyWin;
        _playerWinsCounter.Win += OnPlayerWin;
    }

    private void OnDisable()
    {
        _enemyWinsCounter.Win -= OnEnemyWin;
        _playerWinsCounter.Win -= OnPlayerWin;
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

    private void OnPlayerWin()
    {
        PlayerWin?.Invoke();
    }

    private void OnEnemyWin()
    {
        EnemyWin?.Invoke();
    }
}
