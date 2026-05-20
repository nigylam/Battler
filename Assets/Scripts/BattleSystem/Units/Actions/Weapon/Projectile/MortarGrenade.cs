using UnityEngine;

namespace Battler.BattleSystem.Units.Actions.Weapon
{
    public class MortarGrenade : Projectile
    {
        [SerializeField] private float _arcHeight = 5f;
        [SerializeField] private ParticleSystem _explosionEffect;

        public override void Initialize(LayerMask attackTargets, Vector3 targetVector)
        {
            base.Initialize(attackTargets, targetVector);
            SetVelocity(CalculateBallisticVelocity(targetVector, _arcHeight));
        }

        protected override void Disable()
        {
            _explosionEffect.gameObject.SetActive(false);
        }

        protected override void OnTrigger()
        {
            _explosionEffect.gameObject.SetActive(true);
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
}