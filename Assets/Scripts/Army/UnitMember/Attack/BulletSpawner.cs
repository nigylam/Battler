using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;

    private ObjectPool<Bullet> _pool;
    private List<Bullet> _activeElements = new();
    private int _poolCapacity = 50;
    private int _poolMaxSize = 100;

    private void Awake()
    {
        _pool = new ObjectPool<Bullet>(
            createFunc: () => Instantiate(_bulletPrefab),
            actionOnGet: (obj) => obj.gameObject.SetActive(true),
            actionOnRelease: (obj) => obj.gameObject.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj.gameObject),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
        );
    }

    public Bullet Spawn(Vector3 position, Vector3 shotDirection)
    {
        Bullet bullet = Instantiate(_bulletPrefab, position, Quaternion.identity, transform);
        bullet.Initialize(shotDirection);
        bullet.Collided += OnBulletCollided;
        bullet.Wasted += OnBulletWasted;
        TryAddToActiveList(bullet);
        return bullet;
    }

    public void Restart()
    {
        while (_activeElements.Count > 0)
        {
            ReleaseBullet(_activeElements[0]);
        }

        _activeElements.Clear();
    }

    private void OnBulletCollided(Bullet bullet, UnitMember _)
    {
        ReleaseBullet(bullet);
    }

    private void OnBulletWasted(Bullet bullet)
    {
        ReleaseBullet(bullet);
    }

    private void ReleaseBullet(Bullet bullet)
    {
        if (TryRemoveFromActiveList(bullet) == false)
            return;

        bullet.Collided -= OnBulletCollided;
        bullet.Wasted -= OnBulletWasted;
        _pool.Release(bullet);
    }

    private bool TryAddToActiveList(Bullet bullet)
    {
        if (_activeElements.Contains(bullet))
            return false;

        _activeElements.Add(bullet);
        return true;
    }

    private bool TryRemoveFromActiveList(Bullet bullet)
    {
        if (_activeElements.Contains(bullet) == false)
            return false;

        _activeElements.Remove(bullet);
        return true;
    }
}