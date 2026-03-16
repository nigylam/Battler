using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;

    private float _lifetime = 4f;
    private Rigidbody _rigidbody;

    public event Action<Bullet, UnitMember> Collided;
    public event Action<Bullet> Wasted;

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

    public void Initialize(Vector3 shotDirection)
    {
        _rigidbody.velocity = Vector3.Normalize(shotDirection) * _speed;
    }
}
