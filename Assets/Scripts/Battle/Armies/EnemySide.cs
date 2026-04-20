using System.Collections.Generic;

namespace Battler.Battle.Armies
{
    public class EnemySide : Side
    {
        private List<EnemyRound> _rounds;
        private int _currentRound;
        private EnemyArmyDeployer _deployer;

        protected override ArmyDeployer ArmyDeployer => _deployer;

        protected override void OnAwake()
        {
            _deployer = new EnemyArmyDeployer(Field, Commander, Creator, transform);
        }

        public override void StartLevel(GameContext context)
        {
            _rounds = new();
            _rounds.AddRange(context.Level.Rounds);
            _currentRound = 0;
        }

        protected override void SetRoundAfterPause()
        {
            _deployer.SetRound(_rounds[_currentRound++]);
        }

        protected override void EndRoundPhase1()
        {
            base.EndRoundPhase1();
            EndRoundPhase2();
        }
    }
}