using UnityEngine;

namespace Battler.BattleSystem.Units.Actions.Weapon
{
    public class TankGrenade : Projectile
    {
        [SerializeField] private float _speed;
        [SerializeField] private ParticleSystem _explosionEffect;

        public override void Initialize(LayerMask attackTargets, Vector3 shotDirection)
        {
            base.Initialize(attackTargets, shotDirection);
            SetVelocity(Vector3.Normalize(shotDirection) * _speed);
        }

        protected override void Disable()
        {
            _explosionEffect.gameObject.SetActive(false);
        }

        protected override void OnTrigger()
        {
            _explosionEffect.gameObject.SetActive(true);
            SetVelocity(Vector3.zero);
        }
    }
}
