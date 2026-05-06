using Battler.BattleSystem.Squads;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Battler.BattleSystem.Armies
{
    public abstract class ArmyDeployer
    {
        private const float SpawnWaitTime = 0.5f;


        public event Action DeploymentFinished;

        public ArmyDeployer
        (
            Field field, 
            ArmyCommander commander,
            SquadCreator creator,
            Transform squadsParrent
        )
        {
            Field = field;
            Commander = commander;
            Creator = creator;
            SquadsParrent = squadsParrent;
        }

        protected CancellationToken AsyncCancelToken { get; private set; }
        protected Field Field { get; }
        protected SquadCreator Creator { get; }
        protected Transform SquadsParrent { get; }
        protected ArmyCommander Commander { get; }

        public void SetToken(CancellationToken token)
        {
            AsyncCancelToken = token;
        }

        protected async UniTask Spawn(IEnumerable<ISquadSpawnContext> squads)
        {
            foreach (ISquadSpawnContext squadContext in squads)
            {
                Create(squadContext.Plan, (squadContext.StartCell.x, squadContext.StartCell.y), squadContext.CreateUpgraded);
                await UniTask.Delay(TimeSpan.FromSeconds(SpawnWaitTime), cancellationToken: AsyncCancelToken);
            }
        }

        protected Squad Create(SquadPlan plan, (int x, int y) startCell, bool createUpgraded = false)
        {
            if (plan == null)
                throw new ArgumentNullException(nameof(plan));

            if (Creator.TryCreate(plan, startCell, SquadsParrent, Field, createUpgraded, out Squad squad) == false)
                throw new ArgumentOutOfRangeException(nameof(startCell));

            Commander.Add(squad, plan, startCell, createUpgraded);
            return squad;
        }

        protected void RaiseDeploymentFinished()
        {
            DeploymentFinished?.Invoke();
        }
    }
}
