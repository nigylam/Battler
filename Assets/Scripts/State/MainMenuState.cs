using Battler.UI;

namespace Battler.State
{
    public class MainMenuState : GameState
    {
        private MainMenu _mainMenu;

        public MainMenuState(GameStateMachine stateMachine, GameContext context, MainMenu mainMenu) : base(stateMachine, context)
        {
            _mainMenu = mainMenu;
        }

        public override void Enter()
        {
            _mainMenu.gameObject.SetActive(true);
            _mainMenu.Start += OnStartClick;
            _mainMenu.Settings += OnSettingsClick;
            _mainMenu.Leaderboard += OnLeaderboardClick;
        }

        public override void Exit()
        {
            _mainMenu.gameObject.SetActive(false);
            _mainMenu.Start -= OnStartClick;
            _mainMenu.Settings -= OnSettingsClick;
            _mainMenu.Leaderboard -= OnLeaderboardClick;
        }

        private void OnStartClick()
        {
            StateMachine.ChangeState(GameStateType.LevelMap);
        }

        private void OnSettingsClick()
        {
            StateMachine.PushState(GameStateType.Settings);
        }

        private void OnLeaderboardClick()
        {
            StateMachine.PushState(GameStateType.Leaderboard);
        }
    }
}