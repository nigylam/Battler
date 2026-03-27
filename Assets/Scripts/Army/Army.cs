using System;
using System.Collections.Generic;
using UnityEngine;

public class Army : MonoBehaviour
{
    [SerializeField] private Army _enemyArmy;

    private List<Squad> _aliveSquads = new();

    public event Action Lose;

    private void OnEnable()
    {
        _enemyArmy.Lose += OnEnemyArmyLose;

        foreach (var squads in _aliveSquads)
            squads.Dead += OnSquadDead;
    }

    private void OnDisable()
    {
        _enemyArmy.Lose -= OnEnemyArmyLose;

        foreach (var squads in _aliveSquads)
            squads.Dead -= OnSquadDead;
    }

    public void AddSquad(Squad squad)
    {
        _aliveSquads.Add(squad);
        squad.Dead += OnSquadDead;
    }

    public void Attack()
    {
        foreach (var squad in _aliveSquads)
        {
            squad.Attack(_enemyArmy);
        }
    }

    public List<Unit> GetTargets()
    {
        List<Unit> targets = new();

        foreach (var unit in _aliveSquads)
            targets.AddRange(unit.GetAliveMembers());

        return targets;
    }

    private void OnSquadDead(Squad squad)
    {
        _aliveSquads.Remove(squad);
        squad.Dead -= OnSquadDead;

        if (_aliveSquads.Count == 0)
            Lose?.Invoke();
    }

    private void OnEnemyArmyLose()
    {
        foreach (var unit in _aliveSquads)
            unit.Win();
    }
}
