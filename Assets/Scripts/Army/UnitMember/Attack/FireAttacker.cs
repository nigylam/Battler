using System.Collections.Generic;
using UnityEngine;

public class FireAttacker : Attacker
{
    [SerializeField] private BulletSpawner _bulletSpawner;
    [SerializeField] private Transform _muzzlePoint;

    private List<Bullet> _spawnedBullets = new List<Bullet>();

    private void OnDisable()
    {
        foreach (var bullet in _spawnedBullets)
            bullet.Collided -= OnBulleteCollide;
    }

    protected override void Attack()
    {
        base.Attack();
        Bullet bullet = _bulletSpawner.Spawn(_muzzlePoint.position, GetDirectionToTarget(_muzzlePoint.position));
        bullet.Collided += OnBulleteCollide;
        _spawnedBullets.Add(bullet);
    }

    private void OnBulleteCollide(Bullet bullet, UnitMember unitMember)
    {
        if (IsInLayerMask(unitMember.gameObject) == false)
            return;

        bullet.Collided -= OnBulleteCollide;
        _spawnedBullets.Remove(bullet);
        TakeDamage(unitMember);
    }
}
