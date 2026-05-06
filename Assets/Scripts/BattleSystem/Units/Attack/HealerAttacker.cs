using UnityEngine;

public class HealerAttacker : Attacker
{
    [SerializeField] private HealerDamager _healer;

    public override void Initialize(LayerMask attackTargets)
    {
        _healer.Initialize(attackTargets);
    }

    public override void Upgrade()
    {
        _healer.Upgrade();
    }

    protected override void Attack()
    {
        base.Attack();
        _healer.Heal();
    }
}
