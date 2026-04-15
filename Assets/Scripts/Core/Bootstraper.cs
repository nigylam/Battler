using Battler.UI.BattleView;
using System.Collections.Generic;
using UnityEngine;

namespace Battler.Core
{
    public class Bootstraper : MonoBehaviour
    {
        [SerializeField] private MainMenu _mainMenu;
        [SerializeField] private LevelMenu _levelMenu;
        [SerializeField] private ShopMenu _shopMenu;
        [SerializeField] private BattleEndScreen _battleEndScreen;
        [SerializeField] private BattlePauseMenu _battlePauseMenu;
        [SerializeField] private Battle _battle;
        [SerializeField] private StartSquadsConfig _startSquadSet;
        [SerializeField] private ShopSet _shopSet;
        [SerializeField] private List<LevelConfig> _levelConfigs;

        private void Awake()
        {
            SquadKeeper squadKeeper = SquadKeeperFabric.Create(_startSquadSet);
            var gold = new Gold();
            Shop shop = Create();
            LevelProgress levelProgress = new LevelProgress(_levelConfigs);
            _levelMenu.Initialize(gold);
            _shopMenu.Initialize(gold, shop, squadKeeper);

            var context = new GameContext
            (
                squadKeeper,
                gold,
                shop,
                levelProgress,
                _mainMenu,
                _levelMenu,
                _shopMenu,
                _battleEndScreen,
                _battlePauseMenu,
                _battle
            );

            var stateMachine = new GameStateMachine(context);
            stateMachine.ChangeState(GameStateType.MainMenu);
        }

        private Shop Create()
        {
            List<SquadGoodConfig> goods = new();
            goods.AddRange(_shopSet.Goods);
            return new Shop(goods);
        }
    }
}

