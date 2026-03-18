using UnityEngine;

public class Grenade : Projectile
{
    [SerializeField] private float _arcHeight = 5f;

    public override void Initialize(Vector3 targetVector)
    {
        SetVelocity(CalculateBallisticVelocity(targetVector, _arcHeight));
    }

    private Vector3 CalculateBallisticVelocity(Vector3 displacement, float addedHeight)
    {
        float gravity = Physics.gravity.y;
        float h = Mathf.Max(addedHeight, displacement.y + 1f);
        float velocityY = Mathf.Sqrt(-2 * gravity * h);
        float timeToApex = Mathf.Sqrt(-2 * h / gravity);
        float timeToFall = Mathf.Sqrt(2 * (displacement.y - h) / gravity);
        float totalTime = timeToApex + timeToFall;
        Vector3 velocityXZ = new Vector3(displacement.x, 0, displacement.z) / totalTime;
        return velocityXZ + Vector3.up * velocityY;
    }
}