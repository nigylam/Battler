using Battler.BattleSystem.Units;
using System;
using UnityEngine;

namespace Battler.BattleSystem.Units.Actions.Weapon
{
    public abstract class Projectile : Damager
    {
        [SerializeField] private float _lifetime = 4f;
        [SerializeField] private Rigidbody _rigidbody;

        public event Action<Projectile> Collided;
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
            if (other.TryGetComponent(out Unit unitMember) == false)
                return;

            if (IsInLayerMask(unitMember.gameObject) == false)
                return;

            ApplyDamage(unitMember);
            Collided?.Invoke(this);
        }

        public virtual void Initialize(LayerMask attackTargets, Vector3 shotDirection)
        {
            base.Initialize(attackTargets);
        }

        protected void SetVelocity(Vector3 velocity)
        {
            _rigidbody.velocity = velocity;
        }
    }
}