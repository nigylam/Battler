using Battler.State;
using Battler.UI.ShopView;

public class ShopState : GameState
{
    private ShopMenu _shopMenu;

    public ShopState(GameStateMachine stateMachine, GameContext context, ShopMenu shopMenu) : base(stateMachine, context)
    {
        _shopMenu = shopMenu;
    }

    public override void Enter()
    {
        _shopMenu.gameObject.SetActive(true);
        _shopMenu.Exit += ExitShop;
    }

    public override void Exit()
    {
        _shopMenu.gameObject.SetActive(false);
        _shopMenu.Exit -= ExitShop;
    }

    private void ExitShop()
    {
        StateMachine.ChangeState(GameStateType.LevelMap);
    }
}
