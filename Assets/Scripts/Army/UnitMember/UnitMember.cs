using System;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Attacker))]
[RequireComponent(typeof(MemberAnimator))]
public abstract class UnitMember : MonoBehaviour
{
    [SerializeField] private SmoothSliderBar _healthBar;

    private Health _health;
    private Mover _mover;
    private Attacker _attacker;
    private MemberAnimator _animator;

    public event Action<UnitMember> Dead;
    public event Action<UnitMember> Free;

    public bool IsAlive { get; private set; }

    private void Awake()
    {
        _health = GetComponent<Health>();
        _mover = GetComponent<Mover>();
        _attacker = GetComponent<Attacker>();
        _animator = GetComponent<MemberAnimator>();

        _healthBar.Initialize(_health);
        _healthBar.Enable();
    }

    private void OnEnable()
    {
        _health.Dead += OnDead;
        _attacker.AttackStarted += OnAttackStarted;
        _mover.WentToTarget += OnWentToTarget;
        _mover.LeaveTarget += OnLeaveTarget;
        _attacker.TargetDead += OnTargetDead;
        IsAlive = true;
    }

    private void Update()
    {
        _animator.ProcessMovingAnimations(_mover.Speed > 0);
    }

    private void OnDisable()
    {
        _health.Dead -= OnDead;
        _attacker.AttackStarted -= OnAttackStarted;
        _mover.WentToTarget -= OnWentToTarget;
        _mover.LeaveTarget -= OnLeaveTarget;
    }

    public void TakeDamage(int damage)
    {
        _health.TakeDamage(damage);
        _animator.OnHit();
    }

    public void SetTarget(UnitMember target)
    {
        _mover.SetTarget(target.transform);
    }

    public void Win()
    {
        _mover.Disable();
        _animator.OnWin();
    }

    private void OnWentToTarget()
    {
        _attacker.Attack();
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
        _animator.OnDeath();
        IsAlive = false;
        Dead?.Invoke(this);
    }

    private void OnAttackStarted()
    {
        _animator.OnAttack();
    }
}
