using System;
using System.Collections;
using UnityEngine;

public abstract class Attacker : MonoBehaviour
{
    [SerializeField] private float _cooldownTime;

    private Coroutine _cooldown;
    private Transform _target;
    private float _coolDownOffset = 0.6f;
    private bool _closeToTarget = false;
    private bool _cooldownEnded = true;

    protected virtual bool CanAttack { get; } = true;

    public event Action AttackStarted;
    public event Action AttackEnded;

    private void Update()
    {
        if (_closeToTarget == false) 
            return;

        if(_cooldownEnded == false) 
            return;

        if (CanAttack == false)
            return;

        Attack();
    }

    public abstract void Initialize(LayerMask attackTargets);
    public abstract void Upgrade();

    public virtual void StartAttack()
    {
        _closeToTarget = true;
    }

    public void StopAttack()
    {
        _closeToTarget = false;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    protected virtual void Attack()
    {
        AttackStarted?.Invoke();
        StartCooldown();
    }

    protected Vector3 GetDirectionToTarget(Vector3 startPosition)
    {
        Vector3 shotDirection = _target.position - startPosition;
        shotDirection.y = 0;
        return shotDirection;
    }

    private void StartCooldown()
    {
        _cooldownEnded = false;

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

        _cooldownEnded = true;
        AttackEnded?.Invoke();
    }
}
