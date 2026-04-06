using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleState : GameState
{
    public BattleState(GameStateMachine stateMachine, GameContext context) : base(stateMachine, context){}

    public override void Enter()
    {
        Context.Battle.StartLevel();
    }

    public override void Exit()
    {
        Context.Battle.EndLevel();
    }

    private void StartGame(Level level)
    {
        StateMachine.ChangeState(GameStateType.Battle);
    }
}
