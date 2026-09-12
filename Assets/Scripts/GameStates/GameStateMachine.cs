using Battler.Core;
using Battler.Core.SquadKeeping;
using Battler.Meta;
using Battler.UI.BattleView;
using Battler.UI.ShopView;
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
            UI.MainMenu mainMenu,
            UI.LevelView.LevelMenu levelMenu,
            ShopMenu shopMenu,
            BattleEndScreen battleEndScreen,
            BattlePauseMenu battlePauseMenu,
            UI.SettingsMenu settingsMenu,
            UI.LeaderboardPannel leaderboardPannel,
            UI.ApprovePopup quitApprovePopup,
            Battle battle,
            Rewarder rewarder,
            LevelProgress levelProgress,
            GameSquadKeeper squadKeeper
        )
        {
            _states = new()
            {
                {GameStateType.MainMenu, new MainMenuState(this, mainMenu) },
                {GameStateType.LevelMap, new LevelMapState(this, levelMenu, levelProgress) },
                {GameStateType.Battle, new BattleState(this, battle, squadKeeper) },
                {GameStateType.BattleEnd, new BattleEndState(this, battleEndScreen, rewarder) },
                {GameStateType.Shop, new ShopState(this, shopMenu) },
                {GameStateType.BattlePause, new BattlePauseState(this, battlePauseMenu, battle) },
                {GameStateType.Settings, new SettingsState(this, settingsMenu) },
                {GameStateType.Leaderboard, new LeaderboardState(this, leaderboardPannel) },
                {GameStateType.WinGame, new WinGameState(this, leaderboardPannel) },
                {GameStateType.QuitApprove, new QuitApproveState(this, quitApprovePopup) }
            };
        }

        public void ChangeState(GameStateType stateType)
        {
            ChangeState(stateType, new GameContext());
        }

        public void ChangeState(GameStateType stateType, GameContext context)
        {
            GameState state = _states[stateType];

            while (_statesStack.Count > 0)
            {
                _statesStack.Pop().Exit();
            }

            _statesStack.Push(state);
            state.Enter(context);
        }

        public void PushState(GameStateType stateType)
        {
            PushState(stateType, new GameContext());
        }

        public void PushState(GameStateType stateType, GameContext context)
        {
            GameState state = _states[stateType];
            _statesStack.Push(state);
            state.Enter(context);
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