using System.Collections.Generic;
using UnityEngine;

public class FireAttacker : Attacker
{
    [SerializeField] private ProjectileSpawner _projectileSpawner;
    [SerializeField] private Transform _muzzlePoint;

    private List<Projectile> _spawnedProjectiles = new List<Projectile>();

    private void OnDisable()
    {
        foreach (var bullet in _spawnedProjectiles)
            bullet.Collided -= OnProjectileCollide;
    }

    protected override void Attack()
    {
        base.Attack();
        Projectile projectile = _projectileSpawner.Spawn(_muzzlePoint.position, GetDirectionToTarget(_muzzlePoint.position));
        projectile.Collided += OnProjectileCollide;
        _spawnedProjectiles.Add(projectile);
    }

    private void OnProjectileCollide(Projectile projectile, Unit unitMember)
    {
        if (IsInLayerMask(unitMember.gameObject) == false)
            return;

        projectile.Collided -= OnProjectileCollide;
        _spawnedProjectiles.Remove(projectile);
        TakeDamage(unitMember);
    }
}
