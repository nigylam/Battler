using System;
using System.Collections.Generic;
using UnityEngine;

public class Army : MonoBehaviour
{
    [SerializeField] private Army _enemyArmy;

    private List<Squad> _aliveSquads = new();

    public event Action LoseRound;
    public event Action WinRound;

    public IReadOnlyCollection<Squad> AliveSquads => _aliveSquads;

    private void OnEnable()
    {
        _enemyArmy.LoseRound += OnEnemyArmyLose;

        foreach (var squads in _aliveSquads)
            squads.Dead += OnSquadDead;
    }

    private void OnDisable()
    {
        _enemyArmy.LoseRound -= OnEnemyArmyLose;

        foreach (var squads in _aliveSquads)
            squads.Dead -= OnSquadDead;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.F))
        {
            if(gameObject.layer == 7)
                KillAll();
        }
    }

    private void KillAll()
    {
        for(int i = _aliveSquads.Count - 1; i >= 0; i--)
        {
            _aliveSquads[i].KillAll();
        }
    }

    public void Add(Squad squad)
    {
        _aliveSquads.Add(squad);
        squad.Dead += OnSquadDead;
    }

    public void Clear()
    {
        if (_aliveSquads.Count == 0)
            return;

        while(_aliveSquads.Count > 0)
        {
            Remove(_aliveSquads[0]);
        }
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
            LoseRound?.Invoke();
    }

    private void OnEnemyArmyLose()
    {
        foreach (var unit in _aliveSquads)
            unit.Win();

        WinRound?.Invoke();
    }

    private void Remove(Squad squad)
    {
        squad.Dead -= OnSquadDead;
        _aliveSquads.Remove(squad);
        Destroy(squad.gameObject);
    }
}
