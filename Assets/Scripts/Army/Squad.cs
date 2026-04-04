using System;
using System.Collections.Generic;
using UnityEngine;

public class Squad : MonoBehaviour
{
    private List<Unit> _deadUnits = new();
    private List<Unit> _unitsAlive = new();
    private Army _enemyArmy;

    private bool _isBattleEnded;

    public event Action<Squad> Dead;

    private void OnEnable()
    {
        if (_unitsAlive.Count > 0)
        {
            foreach (Unit unit in _unitsAlive)
            {
                unit.Dead += OnUnitDead;
                unit.Free += OnUnitFree;
            }
        }
    }

    private void OnDisable()
    {
        if (_unitsAlive.Count > 0)
        {
            foreach (Unit unit in _unitsAlive)
            {
                unit.Dead -= OnUnitDead;
                unit.Free -= OnUnitFree;
            }
        }
    }

    public void AddUnit(Unit unit)
    {
        _deadUnits.Add(unit);
        _unitsAlive.Add(unit);
        unit.Dead += OnUnitDead;
        unit.Free += OnUnitFree;
    }

    public void Win()
    {
        _isBattleEnded = true;

        foreach (var unit in _unitsAlive)
            unit.Win();
    }

    public void Upgrade()
    {
        foreach (var unit in _unitsAlive)
            unit.Upgrade();
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
        unit.Dead -= OnUnitDead;
        unit.Free -= OnUnitFree;

        if (_unitsAlive.Count == 0)
        {
            _isBattleEnded = true;
            Dead?.Invoke(this);
        }
    }

    private void OnUnitFree(Unit unit)
    {
        if (_isBattleEnded)
        {

            return;
        }

        unit.SetTarget(_enemyArmy.GetTargets());
    }
}