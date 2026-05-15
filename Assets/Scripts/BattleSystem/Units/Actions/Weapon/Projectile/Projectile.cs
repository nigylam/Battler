using Battler.BattleSystem.Units;
using System;
using System.Collections;
using UnityEngine;

namespace Battler.BattleSystem.Units.Actions.Weapon
{
    public abstract class Projectile : Damager
    {
        [SerializeField] private float _lifetime = 4f;
        [SerializeField] private Rigidbody _rigidbody;

        private readonly float _effectTime = 2f;
        private Coroutine _disableAfterEffect;

        public event Action<Projectile> Collided;
        public event Action<Projectile> Wasted;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            StopDisableCoroutine();
        }

        private void Update()
        {
            _lifetime -= Time.deltaTime;

            if (_lifetime < 0)
                Wasted?.Invoke(this);
        }

        private void OnDisable()
        {
            StopDisableCoroutine();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Unit unitMember) == false)
                return;

            if (IsInLayerMask(unitMember.gameObject) == false)
                return;

            Vector3 hitPoint = other.ClosestPoint(transform.position);
            ApplyDamage(unitMember, hitPoint);
            OnTrigger();
            StopDisableCoroutine();
            _disableAfterEffect = StartCoroutine(DisableAfterEffect());
        }

        public virtual void Initialize(LayerMask attackTargets, Vector3 shotDirection)
        {
            base.Initialize(attackTargets);
        }

        protected void SetVelocity(Vector3 velocity)
        {
            _rigidbody.velocity = velocity;
        }

        protected virtual void OnTrigger() { }

        private void StopDisableCoroutine()
        {
            if (_disableAfterEffect != null)
                StopCoroutine(_disableAfterEffect);
        }

        private IEnumerator DisableAfterEffect()
        {
            float time = 0;

            while(time < _effectTime)
            {
                time += Time.deltaTime;
                yield return null;
            }

            Collided?.Invoke(this);
        }
    }
}