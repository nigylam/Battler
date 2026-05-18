using Battler.BattleSystem.Units;
using System.Collections.Generic;
using UnityEngine;

namespace Battler.BattleSystem.Units.TargetFinding
{
    public class ClosestTargetFinder : TargetFinder
    {
        public override Unit GetTarget(List<Unit> enemies, List<Unit> alies)
        {
            Unit closestTarget = enemies[0];
            float closestTargetSqrDistance = Vector3.SqrMagnitude(closestTarget.transform.position - transform.position);

            foreach (var target in enemies)
            {
                float targetSqrDistance = Vector3.SqrMagnitude(target.transform.position - transform.position);

                if (closestTargetSqrDistance > targetSqrDistance)
                {
                    closestTarget = target;
                }
            }

            return closestTarget;
        }
    }
}