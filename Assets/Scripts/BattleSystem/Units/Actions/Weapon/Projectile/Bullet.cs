using UnityEngine;

namespace Battler.BattleSystem.Units.Actions.Weapon
{
    public class Bullet : Projectile
    {
        [SerializeField] private float _speed;

        public override void Initialize(LayerMask attackTargets, Vector3 shotDirection)
        {
            base.Initialize(attackTargets, shotDirection);
            SetVelocity(Vector3.Normalize(shotDirection) * _speed);
        }

        protected override void OnTrigger()
        {
            SetVelocity(Vector3.zero);
        }
    }
}