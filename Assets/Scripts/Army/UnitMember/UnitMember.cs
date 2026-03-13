using System;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

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
    private UnitMember _target;

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
        _mover.WentToTarget += StartAttack;
        _mover.LeaveTarget += StopAttack;
        _attacker.AttackStarted += OnAttackStarted;
    }

    private void Update()
    {
        _animator.ProcessMovingAnimations(_mover.Speed > 0);
    }

    private void OnDisable()
    {
        _health.Dead -= OnDead;
        _mover.WentToTarget -= StartAttack;
        _mover.LeaveTarget -= StopAttack;
        _attacker.AttackStarted -= OnAttackStarted;

        if (_target != null)
        {
            _target.Dead += OnTargetDead;
            _target = null;
        }
    }

    public void TakeDamage(int damage)
    {
        _health.TakeDamage(damage);
        _animator.OnHit();
    }

    public void SetTarget(UnitMember target)
    {
        _target = target;
        _target.Dead += OnTargetDead;
        _mover.SetTarget(target.transform);
    }

    private void OnTargetDead(UnitMember _)
    {
        _target.Dead -= OnTargetDead;
        _target = null;

        if (IsAlive)
            Free?.Invoke(this);
    }

    private void OnDead()
    {
        IsAlive = false;
        _mover.Disable();
        _animator.OnDeath();
        Dead?.Invoke(this);
    }

    private void StartAttack()
    {
        _attacker.Enable();
    }

    private void StopAttack()
    {
        _attacker.Disable();
    }

    private void OnAttackStarted()
    {
        _animator.OnAttack();
    }
}
