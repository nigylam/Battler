using UnityEngine;

public class Bullet : Projectile
{
    [SerializeField] private float _speed;

    public override void Initialize(Vector3 shotDirection)
    {
        SetVelocity(Vector3.Normalize(shotDirection) * _speed);
    }
}