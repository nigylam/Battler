using Battler.Battle.Squads;
using System;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Battler.Battle.Armies
{
    public class EnemyArmyDeployer : ArmyDeployer
    {
        private float _spawnWaitTime = 0.5f;

        public EnemyArmyDeployer(Field field, ArmyCommander commander, SquadCreator creator, Transform squadsParrent) 
            : base(field, commander, creator, squadsParrent) { }

        public async void SetRound(EnemyRound round)
        {
            if(round == null || round.Squads == null)
                throw new ArgumentNullException(nameof(round));

            foreach (EnemySquad enemySquadData in round.Squads)
            {
                Create(enemySquadData.Squad, (enemySquadData.PositionX, enemySquadData.PositionY));
                await UniTask.Delay(TimeSpan.FromSeconds(_spawnWaitTime));
            }

            RaiseDeploymentFinished();
        }
    }
}
