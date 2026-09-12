using Battler.BattleSystem;
using Battler.Meta;
using Battler.UI.LevelView;
using UnityEngine;

namespace Battler.State
{
    public class LevelMapState : GameState
    {
        private readonly LevelMenu _levelMenu;
        private readonly LevelProgress _levelProgress;

        public LevelMapState(GameStateMachine stateMachine, LevelMenu levelMenu, LevelProgress levelProgress) : base(stateMachine)
        {
            _levelMenu = levelMenu;
            _levelProgress = levelProgress;
        }

        public override void Enter(GameContext context)
        {
            _levelMenu.Enable(_levelProgress);

            if (_levelMenu.ShowWinGame)
                ShowWinGame();

            _levelMenu.Start += OnLevelClick;
            _levelMenu.Shop += OnShopClick;
            _levelMenu.Settings += OnSettingsClick;
            _levelMenu.MainMenu += OnMainMenuClick;
        }

        public override void Exit()
        {
            _levelMenu.gameObject.SetActive(false);
            _levelMenu.Start -= OnLevelClick;
            _levelMenu.Shop -= OnShopClick;
            _levelMenu.Settings -= OnSettingsClick;
            _levelMenu.MainMenu -= OnMainMenuClick;
        }

        private void OnLevelClick(LevelConfig level)
        {
            if (_levelProgress.Opened(level) == false)
                return;

            var levelSettings = new LevelSettings(false, level.Rounds);
            var context = new GameContext(levelSettings, level);
            StateMachine.ChangeState(GameStateType.Battle, context);
        }

        private void OnShopClick()
        {
            StateMachine.ChangeState(GameStateType.Shop);
        }

        private void OnSettingsClick()
        {
            StateMachine.PushState(GameStateType.Settings);
        }

        private void OnMainMenuClick()
        {
            StateMachine.ChangeState(GameStateType.MainMenu);
        }

        private void ShowWinGame()
        {
            StateMachine.PushState(GameStateType.WinGame);
        }
    }
}