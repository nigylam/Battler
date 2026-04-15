using Battler;
using Battler.State;

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
        if (Context.LevelProgress.Opened(level) == false)
            return;

        Context.SetLevel(level);
        StateMachine.ChangeState(GameStateType.Battle);
    }

    private void OpenShop()
    {
        StateMachine.ChangeState(GameStateType.Shop);
    }
}
