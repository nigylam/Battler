using System.Collections.Generic;
using UnityEngine;

namespace Battler.State
{
    public class GameStateMachine
    {
        private readonly Stack<GameState> _statesStack = new();
        private readonly Dictionary<GameStateType, GameState> _states;

        public GameStateMachine
        (
            GameContext context, 
            UI.MainMenu mainMenu, 
            UI.LevelView.LevelMenu levelMenu, 
            ShopMenu shopMenu, 
            BattleEndScreen battleEndScreen, 
            UI.BattleView.BattlePauseMenu battlePauseMenu, 
            UI.SettingsMenu settingsMenu
        )
        {
            _states = new()
            {
                {GameStateType.MainMenu, new MainMenuState(this, context, mainMenu) },
                {GameStateType.LevelMap, new LevelMapState(this, context, levelMenu) },
                {GameStateType.Battle, new BattleState(this, context) },
                {GameStateType.BattleEnd, new BattleEndState(this, context, battleEndScreen) },
                {GameStateType.Shop, new ShopState(this, context, shopMenu) },
                {GameStateType.BattlePause, new BattlePauseState(this, context, battlePauseMenu) },
                {GameStateType.Settings, new SettingsState(this, context, settingsMenu) }
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
}