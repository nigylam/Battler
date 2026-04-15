
using Battler;
using Battler.State;

public class MainMenuState : GameState
{
    public MainMenuState(GameStateMachine stateMachine, GameContext context) : base(stateMachine, context) {}

    public override void Enter()
    {
        Context.MainMenu.gameObject.SetActive(true);
        Context.MainMenu.Start += StartGame;
    }

    public override void Exit()
    {
        Context.MainMenu.gameObject.SetActive(false);
        Context.MainMenu.Start -= StartGame;
    }

    private void StartGame()
    {
        StateMachine.ChangeState(GameStateType.LevelMap);
    }
}
