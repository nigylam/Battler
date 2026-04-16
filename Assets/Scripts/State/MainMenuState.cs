namespace Battler.State
{
    public class MainMenuState : GameState
    {
        public MainMenuState(GameStateMachine stateMachine, GameContext context) : base(stateMachine, context) { }

        public override void Enter()
        {
            Context.MainMenu.gameObject.SetActive(true);
            Context.MainMenu.Start += OnStartClick;
            Context.MainMenu.Settings += OnSettingsClick;
        }

        public override void Exit()
        {
            Context.MainMenu.gameObject.SetActive(false);
            Context.MainMenu.Start -= OnStartClick;
            Context.LevelMenu.Settings -= OnSettingsClick;
        }

        private void OnStartClick()
        {
            StateMachine.ChangeState(GameStateType.LevelMap);
        }

        private void OnSettingsClick()
        {
            StateMachine.PushState(GameStateType.Settings);
        }
    }
}