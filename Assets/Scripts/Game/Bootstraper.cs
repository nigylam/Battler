using UnityEngine;

public class Bootstraper : MonoBehaviour
{
    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private LevelMenu _levelMenu;
    [SerializeField] private BattleEndScreen _battleEndScreen;
    [SerializeField] private Battle _battle;

    private GameStateMachine _stateMachine;
    private GameContext _context;

    private void Awake()
    {
        _context = new GameContext
        (
            _mainMenu,
            _levelMenu,
            _battleEndScreen,
            _battle
        );

        _stateMachine = new GameStateMachine(_context);
        _stateMachine.ChangeState(GameStateType.MainMenu);
    }
}
