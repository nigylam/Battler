using UnityEngine;

public class ProjectileAttacker : Attacker
{
    [SerializeField] private ProjectileSpawner _projectileSpawner;
    [SerializeField] private Transform _muzzlePoint;

    private LayerMask _attackTargets;

    public override void Initialize(LayerMask attackTargets)
    {
        _attackTargets = attackTargets;
    }

    protected override void Attack()
    {
        base.Attack();
        _projectileSpawner.Spawn(_muzzlePoint.position, _attackTargets, GetDirectionToTarget(_muzzlePoint.position));
    }
}
