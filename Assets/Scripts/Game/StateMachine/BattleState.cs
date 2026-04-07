using System;

public class BattleState : GameState
{
    public BattleState(GameStateMachine stateMachine, GameContext context) : base(stateMachine, context){}

    public override void Enter()
    {
        Context.Battle.StartLevel();
        Context.Battle.End += OnBattleEnd;
    }

    public override void Exit()
    {
        Context.Battle.EndLevel();
        Context.Battle.End -= OnBattleEnd;
    }

    private void OnBattleEnd()
    {
        StateMachine.PushState(GameStateType.BattleEnd);
    }
}
