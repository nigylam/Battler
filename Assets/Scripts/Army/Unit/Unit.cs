using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [SerializeField] private SmoothSliderBar _healthBar;
    [SerializeField] private Health _health;
    [SerializeField] private Mover _mover;
    [SerializeField] private Attacker _attacker;
    [SerializeField] private TargetFinder _targetFinder;
    [SerializeField] private DeathAnimationEventSender _deadAnimationEventSender;

    public event Action<Unit> Dead;
    public event Action<Unit> Free;

    public bool IsAlive { get; private set; }
    protected bool IsMoving => _mover.Speed > 0;
    protected abstract UnitAnimator Animator { get; }

    private void OnEnable()
    {
        _healthBar.Initialize(_health);
        _healthBar.Enable();

        _health.Dead += OnDead;
        _mover.WentToTarget += OnWentToTarget;
        _mover.LeaveTarget += OnLeaveTarget;
        _attacker.TargetDead += OnTargetDead;
        _attacker.AttackStarted += OnAttackStarted;
        _deadAnimationEventSender.AnimationEnded += OnDeadAnimationPlayed;

        IsAlive = true;
    }

    private void OnDisable()
    {
        _health.Dead -= OnDead;
        _mover.WentToTarget -= OnWentToTarget;
        _mover.LeaveTarget -= OnLeaveTarget;
        _attacker.TargetDead -= OnTargetDead;
        _attacker.AttackStarted -= OnAttackStarted;
        _deadAnimationEventSender.AnimationEnded -= OnDeadAnimationPlayed;
    }

    public void TakeDamage(int damage)
    {
        _health.TakeDamage(damage);
        Animator.OnHit();
    }

    public void SetTarget(List<Unit> targets)
    {
        Unit target = _targetFinder.GetTarget(targets);
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
        _healthBar.gameObject.SetActive(false);
        IsAlive = false;
        Dead?.Invoke(this);
    }

    private void OnDeadAnimationPlayed()
    {
        gameObject.SetActive(false);
    }
}
