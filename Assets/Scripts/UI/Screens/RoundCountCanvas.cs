using System;
using UnityEngine;

public class RoundCountCanvas : MonoBehaviour
{
    [SerializeField] private TextCounter _roundTextCounter;
    [SerializeField] private RoundWinnerPannel _roundWinnerPannel;
    [SerializeField] private RoundWinsPannel _roundWinsPannel;

    private RoundCounter _roundCounter;
    private bool _haveWinner;

    public event Action PlayerWin;
    public event Action EnemyWin;

    private void Awake()
    {
        _roundCounter = new RoundCounter();
    }

    private void OnEnable()
    {
        _roundTextCounter.Initialize(_roundCounter);
        Restart();
        _roundCounter.Increase();
        _roundWinsPannel.EnemyWin += OnEnemyWin;
        _roundWinsPannel.PlayerWin += OnPlayerWin;
    }

    private void OnDisable()
    {
        _roundWinsPannel.EnemyWin -= OnEnemyWin;
        _roundWinsPannel.PlayerWin -= OnPlayerWin;
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
