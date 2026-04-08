using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : MonoBehaviour
{
    [SerializeField] private PlayerSide _playerSide;
    [SerializeField] private EnemySide _enemySide;
    [SerializeField] private BeforeBattleMenu _beforeBattleMenu;
    [SerializeField] private RoundCountCanvas _roundCountCanvas;
    [SerializeField] private CameraMover _cameraMover;

    private Coroutine _processRound;
    private int _roundsToWin = 2;
    private float _roundPause = 2f;
    private bool _haveWinner;

    public event Action End;

    public bool PlayerWin {  get; private set; }

    private void OnEnable()
    {
        _playerSide.SquadCreated += OnPlayerFirstSquadCreated;
        _playerSide.WinRound += OnPlayerWinRound;
        _enemySide.WinRound += OnEnemyWinRound;
        _beforeBattleMenu.PlayButtonClicked += OnPlayButtonClicked;
        _roundCountCanvas.PlayerWin += OnPlayerWin;
        _roundCountCanvas.EnemyWin += OnEnemyWin;
    }

    private void OnDisable()
    {
        _playerSide.SquadCreated -= OnPlayerFirstSquadCreated;
        _playerSide.WinRound -= OnPlayerWinRound;
        _enemySide.WinRound -= OnEnemyWinRound;
        _beforeBattleMenu.PlayButtonClicked -= OnPlayButtonClicked;
        _roundCountCanvas.PlayerWin -= OnPlayerWin;
        _roundCountCanvas.EnemyWin -= OnEnemyWin;
    }

    public void StartLevel(IReadOnlyCollection<EnemyRound> rounds, SquadKeeper squadKeeper)
    {
        _haveWinner = false;
        _enemySide.SetRounds(rounds);
        _playerSide.SetSquads(squadKeeper);
        ProcessRounds();
        _roundCountCanvas.gameObject.SetActive(true);
        _roundCountCanvas.Initialize(_roundsToWin);
        _cameraMover.gameObject.SetActive(true);
    }

    public void EndLevel()
    {
        _roundCountCanvas.gameObject.SetActive(false);
        _cameraMover.gameObject.SetActive(false);
        _playerSide.OnEndLevel();
    }

    private void OnPlayerFirstSquadCreated()
    {
        _beforeBattleMenu.SetPlayButtonActive();
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
        if (_processRound != null)
            StopCoroutine(_processRound);

        _processRound = StartCoroutine(ProcessRoundAfterPause());
    }

    private void ProcessRounds()
    {
        if (_haveWinner)
            return;

        _beforeBattleMenu.gameObject.SetActive(true);
        _playerSide.PrepareToRound();
        _enemySide.PrepareToRound();
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

    private void OnPlayButtonClicked()
    {
        _beforeBattleMenu.gameObject.SetActive(false);
        _playerSide.Attack();
        _enemySide.Attack();
    }

    private IEnumerator ProcessRoundAfterPause()
    {
        float time = 0;

        while (time < _roundPause)
        {
            time += Time.deltaTime;
            yield return null;
        }

        ProcessRounds();
    }
}
