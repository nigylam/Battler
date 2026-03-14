using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] private MeleeWeaponAnimationEventSender _eventSender;
    [SerializeField] private LayerMask _attackTargets;
    [SerializeField] private Collider _collider;

    [SerializeField] private int _damage;
    [SerializeField] private float _cooldownTime;

    private Coroutine _cooldown;

    private bool _damageDid = false;
    private int _damageOffset = 2;
    private float _coolDownOffset = 0.6f;
    private bool _canAttack = false;
    private bool _cooldownEnded = true;
    private bool _isTargetDead;

    public event Action AttackStarted;
    public event Action AttackEnded;
    public event Action TargetDead;

    private void Awake()
    {
        _collider.enabled = false;
    }

    private void OnEnable()
    {
        _eventSender.AttackHitEnable += EnableDamage;
        _eventSender.AttackHitDisable += DisableDamage;
    }

    private void Update()
    {
        if(_canAttack == false) 
            return;

        if(_cooldownEnded == false) 
            return;

        if (_isTargetDead)
        {
            TargetDead?.Invoke();
            _isTargetDead = false;
            return;
        }

        AttackStarted?.Invoke();
        StartCooldown();
    }

    private void OnDisable()
    {
        _eventSender.AttackHitEnable -= EnableDamage;
        _eventSender.AttackHitDisable -= DisableDamage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_collider.enabled == false)
            return;

        if (IsInLayerMask(other.gameObject, _attackTargets) == false)
            return;

        if (other.TryGetComponent(out UnitMember unitMember))
        {
            if (_damageDid)
                return;

            _damageDid = true;
            int damage = UnityEngine.Random.Range(_damage - _damageOffset, _damage + _damageOffset);
            unitMember.TakeDamage(damage);

            if (unitMember.IsAlive == false)
                _isTargetDead = true;
        }
    }

    public void Attack()
    {
        _canAttack = true;
    }

    public void StopAttack()
    {
        _canAttack = false;
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

    private void EnableDamage()
    {
        _damageDid = false;
        _collider.enabled = true;
    }

    private void DisableDamage()
    {
        _collider.enabled = false;
    }

    private bool IsInLayerMask(GameObject obj, LayerMask mask)
    {
        return (mask.value & (1 << obj.layer)) != 0;
    }
}
