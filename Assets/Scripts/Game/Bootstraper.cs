using System.Collections.Generic;
using UnityEngine;

public class Bootstraper : MonoBehaviour
{
    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private LevelMenu _levelMenu;
    [SerializeField] private ShopMenu _shopMenu;
    [SerializeField] private BattleEndScreen _battleEndScreen;
    [SerializeField] private Battle _battle;
    [SerializeField] private StartSquadSet _startSquadSet;
    [SerializeField] private ShopSet _shopSet;

    private void Awake()
    {
        SquadKeeper squadKeeper = SquadKeeperFabric.Create(_startSquadSet);
        var gold = new Gold();
        Shop shop = Create();
        _levelMenu.Initialize(gold);
        _shopMenu.Initialize(gold, shop, squadKeeper);

        var context = new GameContext
        (
            squadKeeper,
            gold,
            shop,
            _mainMenu,
            _levelMenu,
            _shopMenu,
            _battleEndScreen,
            _battle
        );

        var stateMachine = new GameStateMachine(context);
        stateMachine.ChangeState(GameStateType.MainMenu);
    }

    private Shop Create()
    {
        List<Good> goods = new();
        goods.AddRange(_shopSet.Goods);
        return new Shop(goods);
    }
}
