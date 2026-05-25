using Battler.Core.SquadKeeping;
using Battler.Settings;
using Battler.Meta;
using Battler.State;
using Battler.UI;
using Battler.UI.BattleView;
using Battler.UI.LevelView;
using Battler.UI.ShopView;
using System.Collections.Generic;
using UnityEngine;
using YG;
using UnityEngine.Audio;

namespace Battler.Core
{
    public class Bootstraper : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Battle _battle;
        [SerializeField] private AudioMixer _audioMixer;

        [Header("UI Menu")]
        [SerializeField] private MainMenu _mainMenu;
        [SerializeField] private LevelMenu _levelMenu;
        [SerializeField] private ShopMenu _shopMenu;
        [SerializeField] private BattleEndScreen _battleEndScreen;
        [SerializeField] private BattlePauseMenu _battlePauseMenu;
        [SerializeField] private SettingsMenu _settingsMenu;
        [SerializeField] private LeaderboardPannel _leaderboardPannel;
        [SerializeField] private ApprovePopup _quitApprovePopup;

        [Header("Configs")]
        [SerializeField] private StartSquadsConfig _startSquadSet;
        [SerializeField] private ShopSet _shopSet;
        [SerializeField] private List<LevelConfig> _levelConfigs;

        Audio _audio;

        private void Awake()
        {
            GameSquadKeeper squadKeeper = SquadKeeperFabric.Create(_startSquadSet);
            var gold = new Gold();
            Shop shop = CreateShop();
            LevelProgress levelProgress = new (_levelConfigs, YG2.saves.openedLevels);
            _levelMenu.Initialize(gold, levelProgress);
            _shopMenu.Initialize(gold, shop, squadKeeper);
            _audio = new Audio(_audioMixer);
            _settingsMenu.Initialize(new Language("en", "ru", "tr"), _audio);

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
                _leaderboardPannel,
                _quitApprovePopup
            );

            stateMachine.ChangeState(GameStateType.MainMenu);
        }

        private void Start()
        {
            _audio.ApplySavedSettings();
        }

        private Shop CreateShop()
        {
            List<SquadGoodConfig> goods = new();
            goods.AddRange(_shopSet.Goods);
            return new Shop(goods);
        }
    }
}

