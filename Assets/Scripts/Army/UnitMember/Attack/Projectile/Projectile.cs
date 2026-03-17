using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Projectile : MonoBehaviour
{
    [SerializeField] private float _lifetime = 4f;

    private Rigidbody _rigidbody;

    public event Action<Projectile, UnitMember> Collided;
    public event Action<Projectile> Wasted;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _lifetime -= Time.deltaTime;

        if (_lifetime < 0)
            Wasted?.Invoke(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out UnitMember unitMember))
        {
            Collided?.Invoke(this, unitMember);
        }
    }

    public abstract void Initialize(Vector3 shotDirection);

    protected void SetVelocity(Vector3 velocity)
    {
        _rigidbody.velocity = velocity;
    }
}
