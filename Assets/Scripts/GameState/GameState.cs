using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameState
{
    protected GameStateMachine StateMachine { get; }
    protected GameContext Context { get; }

    public GameState(GameStateMachine stateMachine, GameContext context)
    {
        StateMachine = stateMachine;
        Context = context;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Resume() { }
}
