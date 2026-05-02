using Battler.Battle.Squads;
using System;
using UnityEngine;

namespace Battler.Battle.Armies
{
    public abstract class ArmyDeployer
    {
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

        protected Field Field { get; }
        protected SquadCreator Creator { get; }
        protected Transform SquadsParrent { get; }
        protected ArmyCommander Commander { get; }

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
