using Battler.Meta;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battler.State
{
    public abstract class GameState
    {
        public GameState(GameStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }

        protected GameStateMachine StateMachine { get; }

        public virtual void Enter(GameContext context) { }
        public virtual void Exit() { }
        public virtual void Resume() { }
    }
}