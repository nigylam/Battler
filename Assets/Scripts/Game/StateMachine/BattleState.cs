using UnityEngine;

public class BattleState : GameState
{
    public BattleState(GameStateMachine stateMachine, GameContext context) : base(stateMachine, context){}

    public override void Enter()
    {
        Context.Battle.StartLevel(Context.Level.Rounds, Context.SquadKeeper);
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
