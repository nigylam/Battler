public class LevelMapState : GameState
{
    public LevelMapState(GameStateMachine stateMachine, GameContext context) : base(stateMachine, context) { }

    public override void Enter()
    {
        Context.LevelMenu.gameObject.SetActive(true);
        Context.LevelMenu.Start += StartGame;
        Context.LevelMenu.Shop += OpenShop;
    }

    public override void Exit()
    {
        Context.LevelMenu.gameObject.SetActive(false);
        Context.LevelMenu.Start -= StartGame;
        Context.LevelMenu.Shop -= OpenShop;
    }

    private void StartGame(LevelConfig level)
    {
        Context.SetLevel(level);
        StateMachine.ChangeState(GameStateType.Battle);
    }

    private void OpenShop()
    {
        StateMachine.ChangeState(GameStateType.Shop);
    }
}
