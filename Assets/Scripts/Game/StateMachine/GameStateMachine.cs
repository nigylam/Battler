using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameStateMachine
{
    private Stack<GameState> _statesStack = new();
    private Dictionary<GameStateType, GameState> _states;

    public GameStateMachine(GameContext context)
    {
        _states = new()
        {
            {GameStateType.MainMenu, new MainMenuState(this, context) },
            {GameStateType.LevelMap, new LevelMapState(this, context) },
            {GameStateType.Battle, new BattleState(this, context) },
            {GameStateType.BattleEnd, new BattleEndState(this, context) }
        };
    }

    public void ChangeState(GameStateType stateType)
    {
        GameState state = _states[stateType];

        while (_statesStack.Count > 0)
        {
            _statesStack.Pop().Exit();
        }

        _statesStack.Push(state);
        state.Enter();
    }

    public void PushState(GameStateType stateType)
    {
        GameState state = _states[stateType];
        _statesStack.Push(state);
        state.Enter();
    }

    public void PopState()
    {
        if (_statesStack.Count == 0)
            return;

        _statesStack.Pop().Exit();

        if (_statesStack.Count > 0)
            _statesStack.Peek().Resume();
    }
}
