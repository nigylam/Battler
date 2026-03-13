using System.Collections.Generic;
using UnityEngine;

public class Army : MonoBehaviour
{
    [SerializeField] private List<Unit> _units;
    [SerializeField] private Army _enemyArmy;

    private void Start()
    {
        foreach (var unit in _units)
        {
            unit.Attack(_enemyArmy);
        }
    }

    public UnitMember GetClosestTarget(Transform point)
    {
        List<UnitMember> allTargets = new();

        foreach (var unit in _units)
        {
            allTargets.AddRange(unit.GetAliveMembers());
        }

        UnitMember closestTarget = allTargets[0];
        float closestTargetSqrDistance = Vector3.SqrMagnitude(closestTarget.transform.position - point.position);

        foreach (var target in allTargets)
        {
            float targetSqrDistance = Vector3.SqrMagnitude(target.transform.position - point.position);

            if (closestTargetSqrDistance > targetSqrDistance)
            {
                closestTarget = target;
            }
        }

        return closestTarget;
    }
}
