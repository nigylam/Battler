using UnityEngine;

namespace Battler
{
    public static class VelocityCalculator
    {
        private const float ArcHeight = 5f;
        private const float Speed = 15f;

        public static Vector3 CalculateVelocity(VelocityType type, Vector3 direction)
        {
            Vector3 velocity = Vector3.zero;

            switch (type)
            {
                case VelocityType.Ballistic:
                    velocity = CalculateBallisticVelocity(direction);
                    break;
                case VelocityType.Direct:
                    velocity = CalculateDirectVelocity(direction);
                    break;
            }

            return velocity;
        }

        private static Vector3 CalculateBallisticVelocity(Vector3 direction)
        {
            float gravity = Physics.gravity.y;
            float h = Mathf.Max(ArcHeight, direction.y + 1f);
            float velocityY = Mathf.Sqrt(-2 * gravity * h);
            float timeToApex = Mathf.Sqrt(-2 * h / gravity);
            float timeToFall = Mathf.Sqrt(2 * (direction.y - h) / gravity);
            float totalTime = timeToApex + timeToFall;
            Vector3 velocityXZ = new Vector3(direction.x, 0, direction.z) / totalTime;
            return velocityXZ + Vector3.up * velocityY;
        }

        private static Vector3 CalculateDirectVelocity(Vector3 direction)
        {
            return Vector3.Normalize(direction) * Speed;
        }
    }
}
