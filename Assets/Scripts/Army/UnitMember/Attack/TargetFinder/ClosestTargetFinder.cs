using System.Collections.Generic;
using UnityEngine;

public class ClosestTargetFinder : TargetFinder
{
    public override UnitMember GetTarget(List<UnitMember> targets)
    {
        UnitMember closestTarget = targets[0];
        float closestTargetSqrDistance = Vector3.SqrMagnitude(closestTarget.transform.position - transform.position);

        foreach (var target in targets)
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
