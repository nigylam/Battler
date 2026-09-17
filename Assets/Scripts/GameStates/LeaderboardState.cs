using Battler.Core;
using Battler.UI;
using UnityEngine;

namespace Battler.State
{
    public class LeaderboardState : GameState
    {
        private readonly LeaderboardPannel _leaderboardPannel;

        public LeaderboardState(GameStateMachine stateMachine, LeaderboardPannel leaderboardPannel) : base(stateMachine)
        {
            _leaderboardPannel = leaderboardPannel;
        }

        public override void Enter(GameContext context)
        {
            _leaderboardPannel.SetDefaultTitle();
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
