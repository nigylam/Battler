using UnityEngine;

public class MeleeAttacker : Attacker
{
    [SerializeField] private MeleeWeaponAnimationEventSender _weaponAnimationEventSender;
    [SerializeField] private Collider _collider;

    private bool _damageDid = false;

    private void OnEnable()
    {
        _collider.enabled = false;
        _weaponAnimationEventSender.AttackHitEnable += EnableDamage;
        _weaponAnimationEventSender.AttackHitDisable += DisableDamage;
    }

    private void OnDisable()
    {
        _weaponAnimationEventSender.AttackHitEnable -= EnableDamage;
        _weaponAnimationEventSender.AttackHitDisable -= DisableDamage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_collider.enabled == false)
            return;

        if (IsInLayerMask(other.gameObject) == false)
            return;

        if (other.TryGetComponent(out Unit member))
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
