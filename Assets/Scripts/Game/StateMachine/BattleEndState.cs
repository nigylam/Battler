
public class BattleEndState : GameState
{
    public BattleEndState(GameStateMachine stateMachine, GameContext context) : base(stateMachine, context) { }

    public override void Enter()
    {
        Context.BattleEndScreen.gameObject.SetActive(true);

        if (Context.Battle.PlayerWin)
            Context.BattleEndScreen.SetWinText();
        else
            Context.BattleEndScreen.SetLoseText();

        Context.BattleEndScreen.End += OnEndClicked;
    }

    public override void Exit()
    {
        Context.BattleEndScreen.gameObject.SetActive(false);
    }

    private void OnEndClicked()
    {
        StateMachine.ChangeState(GameStateType.LevelMap);
    }
}
