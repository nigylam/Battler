using System;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private UnitMember[] _members;

    private List<UnitMember> _membersAlive = new();
    private Army _enemyArmy;

    private bool _isBattleEnded;

    public event Action<Unit> Dead;

    private void OnEnable()
    {
        _membersAlive.AddRange(_members);

        foreach (UnitMember member in _members)
        {
            member.Dead += OnMemberDead;
            member.Free += OnMemberFree;
        }
    }

    private void OnDisable()
    {
        foreach (UnitMember member in _members)
        {
            member.Dead -= OnMemberDead;
            member.Free -= OnMemberFree;
        }
    }

    public void Win()
    {
        _isBattleEnded = true;

        foreach (var member in _members)
            member.Win();
    }

    public List<UnitMember> GetAliveMembers()
    {
        return _membersAlive;
    }

    public void Attack(Army army)
    {
        _enemyArmy = army;

        foreach (UnitMember member in _membersAlive)
        {
            member.SetTarget(_enemyArmy.GetTargets());
        }
    }

    private void OnMemberDead(UnitMember member)
    {
        _membersAlive.Remove(member);

        if(_membersAlive.Count == 0)
            Dead?.Invoke(this);
    }

    private void OnMemberFree(UnitMember member)
    {
        if (_isBattleEnded)
            return;

        member.SetTarget(_enemyArmy.GetTargets());
    }
}