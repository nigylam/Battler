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
                return GetClosestTarget(alies);

            if(potentialTargets.Contains(_previousTarget) && potentialTargets.Count > 1)
                potentialTargets.Remove(_previousTarget);

            Unit closestTarget = GetClosestTarget(potentialTargets);
            _previousTarget = closestTarget;

            return closestTarget;
        }

        private Unit GetClosestTarget(List<Unit> units)
        {
            Unit closestTarget = units[0];
            float closestTargetSqrDistance = Vector3.SqrMagnitude(closestTarget.transform.position - transform.position);

            foreach (Unit target in units)
            {
                if (target.transform == transform)
                    continue;

                float targetSqrDistance = Vector3.SqrMagnitude(target.transform.position - transform.position);

                if (closestTargetSqrDistance > targetSqrDistance)
                {
                    closestTargetSqrDistance = targetSqrDistance;
                    closestTarget = target;
                }
            }

            return closestTarget;
        }
    }
}
