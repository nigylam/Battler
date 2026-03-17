using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] private Projectile _projectilePrefab;

    private ObjectPool<Projectile> _pool;
    private List<Projectile> _activeElements = new();
    private int _poolCapacity = 50;
    private int _poolMaxSize = 100;

    private void Awake()
    {
        _pool = new ObjectPool<Projectile>(
            createFunc: () => Instantiate(_projectilePrefab),
            actionOnGet: (obj) => obj.gameObject.SetActive(true),
            actionOnRelease: (obj) => obj.gameObject.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj.gameObject),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
        );
    }

    public Projectile Spawn(Vector3 position, Vector3 shotDirection)
    {
        Projectile projectile = Instantiate(_projectilePrefab, position, Quaternion.identity, transform);
        projectile.Initialize(shotDirection);
        projectile.Collided += OnCollided;
        projectile.Wasted += OnWasted;
        TryAddToActiveList(projectile);
        return projectile;
    }

    public void Restart()
    {
        while (_activeElements.Count > 0)
        {
            Release(_activeElements[0]);
        }

        _activeElements.Clear();
    }

    private void OnCollided(Projectile projectile, UnitMember _)
    {
        Release(projectile);
    }

    private void OnWasted(Projectile projectile)
    {
        Release(projectile);
    }

    private void Release(Projectile projectile)
    {
        if (TryRemoveFromActiveList(projectile) == false)
            return;

        projectile.Collided -= OnCollided;
        projectile.Wasted -= OnWasted;
        _pool.Release(projectile);
    }

    private bool TryAddToActiveList(Projectile projectile)
    {
        if (_activeElements.Contains(projectile))
            return false;

        _activeElements.Add(projectile);
        return true;
    }

    private bool TryRemoveFromActiveList(Projectile projectile)
    {
        if (_activeElements.Contains(projectile) == false)
            return false;

        _activeElements.Remove(projectile);
        return true;
    }
}