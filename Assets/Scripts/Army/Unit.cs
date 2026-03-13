using System;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private UnitMember[] _members;

    private List<UnitMember> _membersAlive = new();
    private Army _enemyArmy;

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

    public List<UnitMember> GetAliveMembers()
    {
        return _membersAlive;
    }

    public void Attack(Army army)
    {
        _enemyArmy = army;

        foreach (UnitMember member in _membersAlive)
        {
            member.SetTarget(_enemyArmy.GetClosestTarget(member.transform));
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
        member.SetTarget(_enemyArmy.GetClosestTarget(member.transform));
    }
}