using System.Collections;
using UnityEngine;

public class Battle : MonoBehaviour
{
    [SerializeField] private PlayerSide _playerSide;
    [SerializeField] private EnemySide _enemySide;
    [SerializeField] private BeforeBattleMenu _beforeBattleMenu;
    [SerializeField] private BattleEndScreen _battleEndScreen;
    [SerializeField] private TextCounter _roundTextCounter;
    [SerializeField] private RoundWinnerPannel _roundWinnerPannel;
    [SerializeField] private CameraMover _cameraMover;

    private RoundCounter _roundCounter;
    private Coroutine _processRound;
    private Level _level;
    private float _roundPause = 2f;
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

    private void OnDisable()
    {
        _playerSide.SquadCreated -= OnPlayerFirstSquadCreated;
        _playerSide.WinBattle -= OnEnemyWin;
        _enemySide.WinBattle -= OnPlayerWin;
        _beforeBattleMenu.PlayButtonClicked -= OnPlayButtonClicked;
    }

    public void SetLevel(Level level)
    {
        _level = level;
        _enemySide.SetRounds(level.Rounds);
    }

    public void StartLevel()
    {
        ProcessRounds();
        _cameraMover.gameObject.SetActive(true);
    }

    public void EndLevel()
    {
        _cameraMover.gameObject.SetActive(false);
    }

    private void OnPlayerFirstSquadCreated()
    {
        _beforeBattleMenu.SetPlayButtonActive();
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
