using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Attacker))]
[RequireComponent(typeof(MemberAnimator))]
[RequireComponent(typeof(TargetFinder))]
public abstract class UnitMember : MonoBehaviour
{
    [SerializeField] private SmoothSliderBar _healthBar;

    private Health _health;
    private Mover _mover;
    private Attacker _attacker;
    private TargetFinder _targetFinder;

    public event Action<UnitMember> Dead;
    public event Action<UnitMember> Free;

    public bool IsAlive { get; private set; }
    protected bool IsMoving => _mover.Speed > 0;
    protected abstract MemberAnimator Animator { get; }

    protected virtual void Awake()
    {
        _health = GetComponent<Health>();
        _mover = GetComponent<Mover>();
        _attacker = GetComponent<Attacker>();
        _targetFinder = GetComponent<TargetFinder>();

        _healthBar.Initialize(_health);
        _healthBar.Enable();
    }

    private void OnEnable()
    {
        _health.Dead += OnDead;
        _mover.WentToTarget += OnWentToTarget;
        _mover.LeaveTarget += OnLeaveTarget;
        _attacker.TargetDead += OnTargetDead;
        _attacker.AttackStarted += OnAttackStarted;
        IsAlive = true;
    }

    private void OnDisable()
    {
        _health.Dead -= OnDead;
        _mover.WentToTarget -= OnWentToTarget;
        _mover.LeaveTarget -= OnLeaveTarget;
        _attacker.AttackStarted -= OnAttackStarted;
    }

    public void TakeDamage(int damage)
    {
        _health.TakeDamage(damage);
        Animator.OnHit();
    }

    public void SetTarget(List<UnitMember> targets)
    {
        UnitMember target = _targetFinder.GetTarget(targets);
        _mover.SetTarget(target.transform);
        _attacker.SetTarget(target.transform);
    }

    public void Win()
    {
        _mover.Disable();
        Animator.OnWin();
    }

    protected virtual void OnAttackStarted() { }

    private void OnWentToTarget()
    {
        _attacker.StartAttack();
    }

    private void OnLeaveTarget()
    {
        _attacker.StopAttack();
    }

    private void OnTargetDead()
    {
        _mover.Enable();
        Free?.Invoke(this);
    }

    private void OnDead()
    {
        _mover.Disable();
        Animator.OnDeath();
        _attacker.StopAttack();
        IsAlive = false;
        Dead?.Invoke(this);
    }
}
