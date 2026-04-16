using Battler.UI.LevelView;

namespace Battler.State
{
    public class LevelMapState : GameState
    {
        private readonly LevelMenu _levelMenu;

        public LevelMapState(GameStateMachine stateMachine, GameContext context, LevelMenu levelMenu) : base(stateMachine, context)
        {
            _levelMenu = levelMenu;
        }

        public override void Enter()
        {
            _levelMenu.gameObject.SetActive(true);
            _levelMenu.Start += StartGame;
            _levelMenu.Shop += OnShopClick;
            _levelMenu.Settings += OnSettingsClick;
        }

        public override void Exit()
        {
            _levelMenu.gameObject.SetActive(false);
            _levelMenu.Start -= StartGame;
            _levelMenu.Shop -= OnShopClick;
            _levelMenu.Settings -= OnSettingsClick;
        }

        private void StartGame(LevelConfig level)
        {
            if (Context.LevelProgress.Opened(level) == false)
                return;

            Context.SetLevel(level);
            StateMachine.ChangeState(GameStateType.Battle);
        }

        private void OnShopClick()
        {
            StateMachine.ChangeState(GameStateType.Shop);
        }

        private void OnSettingsClick()
        {
            StateMachine.PushState(GameStateType.Settings);
        }
    }
}