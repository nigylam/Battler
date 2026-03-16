using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttacker : Attacker
{
    [SerializeField] private MeleeWeaponAnimationEventSender _eventSender;
    [SerializeField] private Collider _collider;

    private bool _damageDid = false;

    private void OnEnable()
    {
        _collider.enabled = false;
        _eventSender.AttackHitEnable += EnableDamage;
        _eventSender.AttackHitDisable += DisableDamage;
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

        if (IsInLayerMask(other.gameObject) == false)
            return;

        if (other.TryGetComponent(out UnitMember member))
        {
            if (_damageDid)
                return;

            _damageDid = true;
            TakeDamage(member);
        }
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
}
