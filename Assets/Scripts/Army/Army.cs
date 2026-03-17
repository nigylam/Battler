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

    public List<UnitMember> GetTargets()
    {
        List<UnitMember> targets = new();

        foreach (var unit in _units)
        {
            targets.AddRange(unit.GetAliveMembers());
        }



        return targets;
    }

    private void OnEnemyArmyLose()
    {
        foreach (var unit in _units)
            unit.Win();
    }
}
