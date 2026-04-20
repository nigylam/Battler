using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battler.Battle.Armies
{
    public class EnemyArmyDeployer : ArmyDeployer
    {
        public EnemyArmyDeployer(Field field, ArmyCommander commander, SquadCreator creator, Transform squadsParrent) 
            : base(field, commander, creator, squadsParrent) { }

        public void SetRound(EnemyRound round)
        {
            foreach (EnemySquad enemySquad in round.Squads)
                CreateSquad(enemySquad.Squad, (enemySquad.PositionX, enemySquad.PositionY));

            RaiseDeploymentFinished();
        }
    }
}
