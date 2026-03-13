using System;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;

    private bool _canAttack = false;

    public event Action AttackStarted;

    private void OnEnable()
    {
        _weapon.AttackStarted += RaiseAttackStarted;
    }

    private void OnDisable()
    {
        _weapon.AttackStarted -= RaiseAttackStarted;
    }

    private void Update()
    {
        if (_canAttack)
            Attack();
    }

    public void Enable()
    {
        _canAttack = true;
    }

    public void Disable()
    {
        _canAttack = false;
    }

    private void Attack()
    {
        _weapon.TryAttack();
    }

    private void RaiseAttackStarted()
    {
        AttackStarted?.Invoke();
    }
}
