using System.Collections.Generic;
using UnityEngine;

namespace Battler.BattleSystem.Units.TargetFinding
{
    public class MedicTargetFinder : TargetFinder
    {
        private Unit _previousTarget;

        public override Unit GetTarget(List<Unit> enemies, List<Unit> alies)
        {
            List<Unit> potentialTargets = new();

            foreach (Unit target in alies)
            {
                if (target.transform == transform)
                    continue;

                if(target.HealthDecreased)
                    potentialTargets.Add(target);
            }

            if (potentialTargets.Count == 0)
                return null;

            if(potentialTargets.Contains(_previousTarget) && potentialTargets.Count > 1)
                potentialTargets.Remove(_previousTarget);

            Unit closestTarget = potentialTargets[0];
            float closestTargetSqrDistance = Vector3.SqrMagnitude(closestTarget.transform.position - transform.position);

            foreach (Unit target in potentialTargets)
            {
                float targetSqrDistance = Vector3.SqrMagnitude(target.transform.position - transform.position);

                if (closestTargetSqrDistance > targetSqrDistance)
                {
                    closestTargetSqrDistance = targetSqrDistance;
                    closestTarget = target;
                }
            }

            _previousTarget = closestTarget;

            return closestTarget;
        }
    }
}
