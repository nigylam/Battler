using UnityEngine;

public class MeleeAttacker : Attacker
{
    [SerializeField] private MeleeWeaponAnimationEventSender _weaponAnimationEventSender;
    [SerializeField] private MeleeDamager _damager;

    private void OnEnable()
    {

        _weaponAnimationEventSender.AttackHitEnable += EnableDamage;
        _weaponAnimationEventSender.AttackHitDisable += DisableDamage;
    }

    private void OnDisable()
    {
        _weaponAnimationEventSender.AttackHitEnable -= EnableDamage;
        _weaponAnimationEventSender.AttackHitDisable -= DisableDamage;
    }

    public override void Initialize(LayerMask attackTargets)
    {
        _damager.Initialize(attackTargets);
    }

    public override void Upgrade()
    {
        _damager.Upgrade();
    }

    private void EnableDamage()
    {
        _damager.EnableDamage();
    }

    private void DisableDamage()
    {
        _damager.DisableDamage();
    }
}
