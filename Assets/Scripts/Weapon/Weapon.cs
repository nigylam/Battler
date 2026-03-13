using System;
using System.Collections;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected int Damage;
    [SerializeField] private float _cooldownTime;

    protected bool CanAttack = true;

    private float _coolDownOffset = 0.6f;
    private Coroutine _cooldown;

    public event Action AttackStarted;
    public event Action AttackEnded;

    protected virtual void OnEnable()
    {
        CanAttack = true;
    }

    protected virtual void OnDisable()
    {
        Restart();
    }

    public virtual void Restart()
    {
        CanAttack = true;

        if (_cooldown != null)
            StopCoroutine(_cooldown);
    }

    public bool TryAttack()
    {
        if (CanAttack)
        {
            Attack();
            return true;
        }

        return false;
    }

    protected virtual void Attack()
    {
        AttackStarted?.Invoke();
        StartCooldown();
    }

    private void StartCooldown()
    {
        CanAttack = false;

        if (_cooldown != null)
            StopCoroutine(_cooldown);

        _cooldown = StartCoroutine(AttackCooldown());
    }

    private IEnumerator AttackCooldown()
    {

        float time = 0;

        float cooldown = UnityEngine.Random.Range(_cooldownTime - _coolDownOffset, _cooldownTime + _coolDownOffset);

        while (time < cooldown)
        {
            time += Time.deltaTime;
            yield return null;
        }

        CanAttack = true;
        AttackEnded?.Invoke();
    }
}
