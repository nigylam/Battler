using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private PlayerSide _playerSide;
    [SerializeField] private EnemySide _enemySide;
    [SerializeField] private BeforeBattleMenu _beforeBattleMenu;

    private void OnEnable()
    {
        _playerSide.WinRound += OnRoundEnd;
        _enemySide.WinRound += OnRoundEnd;
        _playerSide.WinBattle += OnPlayerWin;
        _enemySide.WinBattle += OnEnemyWin;
        _beforeBattleMenu.PlayButtonClicked += OnPlayButtonClicked;
    }

    private void Start()
    {
        ProcessRounds();
    }

    private void OnDisable()
    {
        _playerSide.WinBattle -= OnEnemyWin;
        _enemySide.WinBattle -= OnPlayerWin;
        _beforeBattleMenu.PlayButtonClicked -= OnPlayButtonClicked;
    }

    private void OnRoundEnd()
    {
        ProcessRounds();
    }

    private void ProcessRounds()
    {
        _beforeBattleMenu.gameObject.SetActive(true);
        _playerSide.PrepareToRound();
        _enemySide.PrepareToRound();
    }

    private void OnPlayerWin()
    {
        Debug.Log("You win");
    }

    private void OnEnemyWin()
    {
        Debug.Log("Enemy win");
    }

    private void OnPlayButtonClicked()
    {
        _beforeBattleMenu.gameObject.SetActive(false);
        _playerSide.Attack();
        _enemySide.Attack();
    }
}
