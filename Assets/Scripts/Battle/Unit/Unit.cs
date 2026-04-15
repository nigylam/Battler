using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private Health _health;
    [SerializeField] private Mover _mover;
    [SerializeField] private Attacker _attacker;
    [SerializeField] private TargetFinder _targetFinder;
    [SerializeField] private DeathAnimationEventSender _deadAnimationEventSender;
    [SerializeField] private SkinnedMeshRenderer _meshRenderer;

    private Unit _target;
    private bool _dead;

    public event Action<Unit> Dead;
    public event Action<Unit> Free;

    protected bool IsMoving => _mover.Speed > 0;
    protected abstract UnitAnimator Animator { get; }

    public void Upgrade()
    {
        _healthBar.Upgrade();
        _health.Upgrade();
        _mover.Upgrade();
        _attacker.Upgrade();
        Animator.OnUpgrade();
    }

    private void OnEnable()
    {
        _health.Dead += OnDead;
        _mover.WentToTarget += OnWentToTarget;
        _mover.LeaveTarget += OnLeaveTarget;
        _attacker.AttackStarted += OnAttackStarted;
        _deadAnimationEventSender.AnimationEnded += OnDeadAnimationPlayed;
        _mover.Disable();
        

        if (_target != null)
            _target.Dead += OnTargetDead;
    }

    private void OnDisable()
    {
        _mover.Disable();

        _health.Dead -= OnDead;
        _mover.WentToTarget -= OnWentToTarget;
        _mover.LeaveTarget -= OnLeaveTarget;
        _attacker.AttackStarted -= OnAttackStarted;
        _deadAnimationEventSender.AnimationEnded -= OnDeadAnimationPlayed;

        if (_target != null)
            _target.Dead -= OnTargetDead;
    }

    public void Initialize(Material armyMaterial, LayerMask attackTargets)
    {
        _meshRenderer.material = armyMaterial;
        _attacker.Initialize(attackTargets);
    }

    public void TakeDamage(int damage)
    {
        _health.TakeDamage(damage);
        Animator.OnHit();
    }

    public void Heal(int count)
    {
        _health.Heal(count);
    }

    public void SetTarget(List<Unit> targets)
    {
        if (_dead)
            return;

        if (_target != null)
        {
            _target.Dead -= OnTargetDead;
        }

        _target = _targetFinder.GetTarget(targets);
        _target.Dead += OnTargetDead;

        _mover.Enable();
        _mover.SetTarget(_target.transform);
        _attacker.SetTarget(_target.transform);
    }

    public void Win()
    {
        if(_dead) 
            return;

        _mover.Disable();
        Animator.OnWin();
        _attacker.StopAttack();
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

    private void OnTargetDead(Unit _)
    {
        if(_dead)
            return;

        _target.Dead -= OnTargetDead;
        _target = null;

        _mover.Enable();
        Free?.Invoke(this);
    }

    private void OnDead()
    {
        _mover.Disable();
        Animator.OnDeath();
        _attacker.StopAttack();
        _healthBar.gameObject.SetActive(false);
        _dead = true;
        Dead?.Invoke(this);
    }

    private void OnDeadAnimationPlayed()
    {
        gameObject.SetActive(false);
    }
}
