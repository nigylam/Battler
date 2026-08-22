using Battler.BattleSystem.Units;
using System;
using System.Collections;
using Unity.Collections;
using UnityEngine;

namespace Battler.BattleSystem.Units.Actions.Weapon
{
    public class Projectile : Damager
    {
        [SerializeField] private DamageType _damageType;
        [SerializeField] private VelocityType _projectileType;
        [SerializeField] private ParticleSystem _explosionEffect;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private MeshRenderer _visual;
        [SerializeField] private float _damageRadius;
        [SerializeField] private float _startLifetime = 4f;

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
            if (_explosionEffect != null)
                _explosionEffect.gameObject.SetActive(false);

            if (_disableAfterEffect != null)
                StopCoroutine(_disableAfterEffect);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (ProjectileDamageDealer.TryDealDamage(_damageType, other, transform, _damageRadius, IsInLayerMask, ApplyDamage) == false)
                return;

            if (_explosionEffect != null)
                _explosionEffect.gameObject.SetActive(true);

            _rigidbody.velocity = Vector3.zero;
            _visual.enabled = false;

            if (_disableAfterEffect != null)
                StopCoroutine(_disableAfterEffect);

            _disableAfterEffect = StartCoroutine(DisableAfterEffect());
        }

        public void Initialize(LayerMask attackTargets, Vector3 shotDirection)
        {
            base.Initialize(attackTargets);
            _lifetime = _startLifetime;
            _rigidbody.velocity = VelocityCalculator.CalculateVelocity(_projectileType, shotDirection);
        }

        private IEnumerator DisableAfterEffect()
        {
            float time = 0;

            while (time < _effectTime)
            {
                time += Time.deltaTime;
                yield return null;
            }

            Collided?.Invoke(this);
        }
    }
}