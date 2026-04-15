using Battler;
using Battler.State;

public class ShopState : GameState
{
    public ShopState(GameStateMachine stateMachine, GameContext context) : base(stateMachine, context) { }

    public override void Enter()
    {
        Context.ShopMenu.gameObject.SetActive(true);
        Context.ShopMenu.Exit += ExitShop;
    }

    public override void Exit()
    {
        Context.ShopMenu.gameObject.SetActive(false);
        Context.ShopMenu.Exit -= ExitShop;
    }

    private void ExitShop()
    {
        StateMachine.ChangeState(GameStateType.LevelMap);
    }
}
