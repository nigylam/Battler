
public class LevelMapState : GameState
{
    public LevelMapState(GameStateMachine stateMachine, GameContext context) : base(stateMachine, context) { }

    public override void Enter()
    {
        Context.LevelMenu.gameObject.SetActive(true);
        Context.LevelMenu.Start += StartGame;
    }

    public override void Exit()
    {
        Context.LevelMenu.gameObject.SetActive(false);
        Context.LevelMenu.Start -= StartGame;
    }

    private void StartGame(Level level)
    {
        Context.Battle.SetLevel(level);
        StateMachine.ChangeState(GameStateType.Battle);
    }
}
