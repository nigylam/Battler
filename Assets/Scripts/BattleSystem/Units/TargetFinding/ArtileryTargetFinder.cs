using System.Collections.Generic;
using UnityEngine;

namespace Battler.BattleSystem.Units.TargetFinding
{
    public class ArtileryTargetFinder : TargetFinder
    {
        public override Unit GetTarget(List<Unit> enemies, List<Unit> alies)
        {
            Unit farestTarget = enemies[0];
            float closestTargetSqrDistance = Vector3.SqrMagnitude(farestTarget.transform.position - transform.position);

            foreach (var target in enemies)
            {
                float targetSqrDistance = Vector3.SqrMagnitude(target.transform.position - transform.position);

                if (closestTargetSqrDistance < targetSqrDistance)
                {
                    farestTarget = target;
                }
            }

            return farestTarget;
        }
    }
}