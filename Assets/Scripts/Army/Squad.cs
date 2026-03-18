using System;
using System.Collections.Generic;
using UnityEngine;

public class Squad : MonoBehaviour
{
    [SerializeField] private Unit[] _units;

    private List<Unit> _unitsAlive = new();
    private Army _enemyArmy;

    private bool _isBattleEnded;

    public event Action<Squad> Dead;

    private void OnEnable()
    {
        _unitsAlive.AddRange(_units);

        foreach (Unit unit in _units)
        {
            unit.Dead += OnUnitDead;
            unit.Free += OnUnitFree;
        }
    }

    private void OnDisable()
    {
        foreach (Unit unit in _units)
        {
            unit.Dead -= OnUnitDead;
            unit.Free -= OnUnitFree;
        }
    }

    public void Win()
    {
        _isBattleEnded = true;

        foreach (var unit in _units)
            unit.Win();
    }

    public List<Unit> GetAliveMembers()
    {
        return _unitsAlive;
    }

    public void Attack(Army army)
    {
        _enemyArmy = army;

        foreach (Unit unit in _unitsAlive)
        {
            unit.SetTarget(_enemyArmy.GetTargets());
        }
    }

    private void OnUnitDead(Unit unit)
    {
        _unitsAlive.Remove(unit);

        if(_unitsAlive.Count == 0)
            Dead?.Invoke(this);
    }

    private void OnUnitFree(Unit unit)
    {
        if (_isBattleEnded)
            return;

        unit.SetTarget(_enemyArmy.GetTargets());
    }
}