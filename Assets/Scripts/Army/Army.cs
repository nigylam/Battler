using System;
using System.Collections.Generic;
using UnityEngine;

public class Army : MonoBehaviour
{
    [SerializeField] private List<Squad> _squads;
    [SerializeField] private Army _enemyArmy;

    private List<Squad> _aliveSquads;

    public event Action Lose;

    private void OnEnable()
    {
        _enemyArmy.Lose += OnEnemyArmyLose;
        _aliveSquads = new List<Squad>();
        _aliveSquads.AddRange(_squads);

        foreach (var unit in _squads)
            unit.Dead += OnSquadDead;
    }

    private void OnSquadDead(Squad squad)
    {
        _aliveSquads.Remove(squad);

        if(_aliveSquads.Count == 0)
            Lose?.Invoke();
    }

    private void Start()
    {
        foreach (var squad in _squads)
        {
            squad.Attack(_enemyArmy);
        }
    }

    private void OnDisable()
    {
        _enemyArmy.Lose -= OnEnemyArmyLose;

        foreach (var squads in _squads)
            squads.Dead -= OnSquadDead;
    }

    public List<Unit> GetTargets()
    {
        List<Unit> targets = new();

        foreach (var unit in _squads)
            targets.AddRange(unit.GetAliveMembers());

        return targets;
    }

    private void OnEnemyArmyLose()
    {
        foreach (var unit in _squads)
            unit.Win();
    }
}
