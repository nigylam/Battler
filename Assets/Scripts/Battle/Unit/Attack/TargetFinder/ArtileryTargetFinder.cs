using System.Collections.Generic;
using UnityEngine;

public class ArtileryTargetFinder : TargetFinder
{
    public override Unit GetTarget(List<Unit> targets)
    {
        Unit farestTarget = targets[0];
        float closestTargetSqrDistance = Vector3.SqrMagnitude(farestTarget.transform.position - transform.position);

        foreach (var target in targets)
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
