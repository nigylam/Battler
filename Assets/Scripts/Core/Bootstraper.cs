using Battler.State;
using Battler.UI;
using Battler.UI.BattleView;
using Battler.UI.LevelView;
using System.Collections.Generic;
using UnityEngine;

namespace Battler.Core
{
    public class Bootstraper : MonoBehaviour
    {
        [Header("Battle")]
        [SerializeField] private Battle _battle;

        [Header("UI Menu")]
        [SerializeField] private MainMenu _mainMenu;
        [SerializeField] private LevelMenu _levelMenu;
        [SerializeField] private ShopMenu _shopMenu;
        [SerializeField] private BattleEndScreen _battleEndScreen;
        [SerializeField] private BattlePauseMenu _battlePauseMenu;
        [SerializeField] private SettingsMenu _settingsMenu;
        [SerializeField] private LeaderboardPannel _leaderboardPannel;

        [Header("Configs")]
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
                _battle
            );

            var stateMachine = new GameStateMachine
            (
                context, 
                _mainMenu, 
                _levelMenu, 
                _shopMenu, 
                _battleEndScreen, 
                _battlePauseMenu, 
                _settingsMenu,
                _leaderboardPannel
            );

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

