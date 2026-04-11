using System;
using UnityEngine;

public class Battle : MonoBehaviour
{
    [SerializeField] private PlayerSide _player;
    [SerializeField] private EnemySide _enemy;
    [SerializeField] private RoundCountCanvas _roundCountCanvas;
    [SerializeField] private CameraMover _cameraMover;

    private int _roundsToWin = 2;
    private int _sidesCount = 2;
    private bool _haveWinner;
    private int _sidesReadyForRound;
    private int _sidesEndRound;

    public event Action End;

    public bool PlayerWin {  get; private set; }

    private void OnEnable()
    {
        _player.ReadyForRound += OnReadyForRound;
        _enemy.ReadyForRound += OnReadyForRound;
        _player.RoundEnded += OnRoundEnded;
        _enemy.RoundEnded += OnRoundEnded;
        _player.WinRound += OnPlayerWinRound;
        _enemy.WinRound += OnEnemyWinRound;
        _roundCountCanvas.PlayerWin += OnPlayerWin;
        _roundCountCanvas.EnemyWin += OnEnemyWin;
    }


    private void OnDisable()
    {
        _player.WinRound -= OnPlayerWinRound;
        _enemy.WinRound -= OnEnemyWinRound;
        _roundCountCanvas.PlayerWin -= OnPlayerWin;
        _roundCountCanvas.EnemyWin -= OnEnemyWin;
    }

    public void StartLevel(GameContext context)
    {
        _haveWinner = false;
        _enemy.StartLevel(context);
        _player.StartLevel(context);
        PrepareToRound();
        _roundCountCanvas.gameObject.SetActive(true);
        _roundCountCanvas.Initialize(_roundsToWin);
        _cameraMover.gameObject.SetActive(true);
    }

    public void EndLevel()
    {
        _roundCountCanvas.gameObject.SetActive(false);
        _cameraMover.gameObject.SetActive(false);
        _enemy.EndLevel();
        _player.EndLevel();
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
        if(++_sidesEndRound == _sidesCount)
        {
            PrepareToRound();
            _sidesEndRound = 0;
        }
    }

    private void OnReadyForRound()
    {
        if(++_sidesReadyForRound == _sidesCount)
        {
            _player.StartRound();
            _enemy.StartRound();
            _sidesReadyForRound = 0;
        }
    }

    private void OnPlayerWinRound()
    {
        _roundCountCanvas.OnPlayerWinRound();
        OnRoundEnd();
    }

    private void OnEnemyWinRound()
    {
        _roundCountCanvas.OnEnemyWinRound();
        OnRoundEnd();
    }

    private void OnRoundEnd()
    {
        _player.EndRound();
        _enemy.EndRound();
    }

    private void OnPlayerWin()
    {
        PlayerWin = true;
        _haveWinner = true;
        End?.Invoke();
    }

    private void OnEnemyWin()
    {
        PlayerWin = false;
        _haveWinner = true;
        End?.Invoke();
    }
}
