using System;
using System.Collections;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private PlayerSide _playerSide;
    [SerializeField] private EnemySide _enemySide;
    [SerializeField] private BeforeBattleMenu _beforeBattleMenu;
    [SerializeField] private BattleEndScreen _battleEndScreen;
    [SerializeField] private TextCounter _roundTextCounter;
    [SerializeField] private RoundWinnerPannel _roundWinnerPannel;

    private RoundCounter _roundCounter;
    private float _roundPause = 2f;
    private Coroutine _processRound;
    private bool _haveWinner;

    private void Awake()
    {
        _roundCounter = new RoundCounter(0);
    }

    private void OnEnable()
    {
        _roundTextCounter.Initialize(_roundCounter);

        _playerSide.SquadCreated += OnPlayerFirstSquadCreated;
        _playerSide.WinRound += OnPlayerWinRound;
        _enemySide.WinRound += OnEnemyWinRound;
        _playerSide.WinBattle += OnPlayerWin;
        _enemySide.WinBattle += OnEnemyWin;
        _beforeBattleMenu.PlayButtonClicked += OnPlayButtonClicked;
    }

    private void OnPlayerFirstSquadCreated()
    {
        _beforeBattleMenu.SetPlayButtonActive();
    }

    private void Start()
    {
        ProcessRounds();
    }

    private void OnDisable()
    {
        _playerSide.SquadCreated -= OnPlayerFirstSquadCreated;
        _playerSide.WinBattle -= OnEnemyWin;
        _enemySide.WinBattle -= OnPlayerWin;
        _beforeBattleMenu.PlayButtonClicked -= OnPlayButtonClicked;
    }

    private void OnPlayerWinRound()
    {
        _roundWinnerPannel.SetPlayerWinner();
        OnRoundEnd();
    }

    private void OnEnemyWinRound()
    {
        _roundWinnerPannel.SetEnemyWinner();
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

        _roundCounter.Increase();
        _beforeBattleMenu.gameObject.SetActive(true);
        _playerSide.PrepareToRound();
        _enemySide.PrepareToRound();
    }

    private void OnPlayerWin()
    {
        _haveWinner = true;
        _battleEndScreen.SetWinText();
    }

    private void OnEnemyWin()
    {
        _haveWinner = true;
        _battleEndScreen.SetLoseText();
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
