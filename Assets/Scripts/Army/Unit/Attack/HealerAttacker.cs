using UnityEngine;

public class HealerAttacker : Attacker
{
    [SerializeField] private HealerDamager _healer;

    public override void Initialize(LayerMask attackTargets)
    {
        _healer.Initialize(attackTargets);
    }

    protected override void Attack()
    {
        base.Attack();
        _healer.Heal();
    }

    
}
