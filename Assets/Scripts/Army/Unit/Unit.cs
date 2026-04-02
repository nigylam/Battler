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
    [SerializeField] private SkinnedMeshRenderer _meshRenderer;

    private Unit _target;
    private Vector3 _startPosition;

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
        _attacker.AttackStarted += OnAttackStarted;
        _deadAnimationEventSender.AnimationEnded += OnDeadAnimationPlayed;
        _mover.Disable();

        if (_target != null)
            _target.Dead += OnTargetDead;

        IsAlive = true;
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
        _startPosition = transform.position;
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
        if(_target != null)
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
        _mover.Disable();
        Animator.OnWin();
        _attacker.StopAttack();
    }

    public void Respawn()
    {
        transform.position = _startPosition;
        _health.Restart();
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
        IsAlive = false;
        Dead?.Invoke(this);
    }

    private void OnDeadAnimationPlayed()
    {
        gameObject.SetActive(false);
    }
}
