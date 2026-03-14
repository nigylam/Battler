using System;
using System.Collections.Generic;
using UnityEngine;

public class Army : MonoBehaviour
{
    [SerializeField] private List<Unit> _units;
    [SerializeField] private Army _enemyArmy;

    private List<Unit> _aliveUnits;

    public event Action Lose;

    private void OnEnable()
    {
        _enemyArmy.Lose += OnEnemyArmyLose;
        _aliveUnits = new List<Unit>();
        _aliveUnits.AddRange(_units);

        foreach (var unit in _units)
            unit.Dead += OnUnitDead;
    }

    private void OnUnitDead(Unit unit)
    {
        _aliveUnits.Remove(unit);

        if(_aliveUnits.Count == 0)
            Lose?.Invoke();
    }

    private void Start()
    {
        foreach (var unit in _units)
        {
            unit.Attack(_enemyArmy);
        }
    }

    private void OnDisable()
    {
        _enemyArmy.Lose -= OnEnemyArmyLose;

        foreach (var unit in _units)
            unit.Dead -= OnUnitDead;
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

    private void OnEnemyArmyLose()
    {
        foreach (var unit in _units)
            unit.Win();
    }
}
