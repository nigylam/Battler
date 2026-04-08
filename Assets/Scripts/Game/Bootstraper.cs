using UnityEngine;

public class Bootstraper : MonoBehaviour
{
    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private LevelMenu _levelMenu;
    [SerializeField] private BattleEndScreen _battleEndScreen;
    [SerializeField] private Battle _battle;

    private GameStateMachine _stateMachine;
    private GameContext _context;
    private Gold _gold;

    private void Awake()
    {
        _gold = new Gold();
        _levelMenu.Initialize(_gold);

        _context = new GameContext
        (
            _gold,
            _mainMenu,
            _levelMenu,
            _battleEndScreen,
            _battle
        );

        _stateMachine = new GameStateMachine(_context);
        _stateMachine.ChangeState(GameStateType.MainMenu);
    }
}
