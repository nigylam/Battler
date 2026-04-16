using Battler.UI;
using UnityEngine;

namespace Battler.State
{
    public class WinGameState : GameState
    {
        private readonly LeaderboardPannel _leaderboardPannel;

        public WinGameState(GameStateMachine stateMachine, GameContext context, LeaderboardPannel leaderboardPannel) : base(stateMachine, context)
        {
            _leaderboardPannel = leaderboardPannel;
        }

        public override void Enter()
        {
            _leaderboardPannel.SetWinTitle();
            _leaderboardPannel.Resume += OnResumeClick;
        }

        public override void Exit()
        {
            _leaderboardPannel.Resume -= OnResumeClick;
            _leaderboardPannel.gameObject.SetActive(false);
        }

        private void OnResumeClick()
        {
            StateMachine.PopState();
        }
    }
}
