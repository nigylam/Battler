using Battler.BattleSystem.Units;
using System;
using System.Collections;
using UnityEngine;

namespace Battler.BattleSystem.Units.Actions.Weapon
{
    public abstract class Projectile : Damager
    {
        [SerializeField] private float _startLifetime = 4f;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private MeshRenderer _visual;
        
        private readonly float _effectTime = 2f;

        private float _lifetime;
        private Coroutine _disableAfterEffect;

        public event Action<Projectile> Collided;
        public event Action<Projectile> Wasted;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            if (_disableAfterEffect != null)
                StopCoroutine(_disableAfterEffect);

            _visual.enabled = true;
        }

        private void Update()
        {
            _lifetime -= Time.deltaTime;

            if (_lifetime < 0)
                Wasted?.Invoke(this);
        }

        private void OnDisable()
        {
            if (_disableAfterEffect != null)
                StopCoroutine(_disableAfterEffect);

            Disable();
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

            if (_disableAfterEffect != null)
                StopCoroutine(_disableAfterEffect);
            _disableAfterEffect = StartCoroutine(DisableAfterEffect());
        }

        public virtual void Initialize(LayerMask attackTargets, Vector3 shotDirection)
        {
            base.Initialize(attackTargets);
            _lifetime = _startLifetime;
        }

        protected virtual void OnTrigger() { }
        protected virtual void Disable() { }

        protected void HideVisual()
        {
            _visual.enabled = false;
        }

        protected void SetVelocity(Vector3 velocity)
        {
            _rigidbody.velocity = velocity;
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