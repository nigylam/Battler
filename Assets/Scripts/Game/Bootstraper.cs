using UnityEngine;

public class Bootstraper : MonoBehaviour
{
    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private LevelMenu _levelMenu;
    [SerializeField] private BattleEndScreen _battleEndScreen;
    [SerializeField] private Battle _battle;
    [SerializeField] private StartSquadSet _startSquadSet;

    private void Awake()
    {
        var gold = new Gold();
        _levelMenu.Initialize(gold);

        SquadKeeper squadKeeper = SquadKeeperFabric.Create(_startSquadSet);

        var context = new GameContext
        (
            squadKeeper,
            gold,
            _mainMenu,
            _levelMenu,
            _battleEndScreen,
            _battle
        );

        var stateMachine = new GameStateMachine(context);
        stateMachine.ChangeState(GameStateType.MainMenu);
    }
}
