using Battler.BattleSystem.Squads;
using System;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Battler.BattleSystem.Armies
{
    public class EnemyArmyDeployer : ArmyDeployer
    {
        public EnemyArmyDeployer(Field field, ArmyCommander commander, SquadCreator creator, Transform squadsParrent) 
            : base(field, commander, creator, squadsParrent) { }

        public async UniTask SetRound(EnemyRound round)
        {
            if(round == null || round.Squads == null)
                throw new ArgumentNullException(nameof(round));

            await Spawn(round.Squads);
        }
    }
}
