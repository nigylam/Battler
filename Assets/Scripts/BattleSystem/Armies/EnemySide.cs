using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Battler.BattleSystem.Armies
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

        public void StartLevel(IReadOnlyCollection<EnemyRound> rounds, bool isRoundReplay, CancellationToken cancelToken)
        {
            StartLevel(cancelToken);
            _rounds = new();
            _rounds.AddRange(rounds);

            if (isRoundReplay)
                _currentRound = _rounds.Count - 1;
            else
                _currentRound = 0;
        }

        public override async UniTask SpawnSquads()
        {
            await _deployer.SetRound(_rounds[_currentRound++]);
        }

        public override async UniTask PlayWin()
        {
            Commander.PlayWin();
            await base.PlayWin();
        }
    }
}