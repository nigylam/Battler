using System;
using System.Collections;
using UnityEngine;

namespace Battler.BattleSystem.Units.Actions.Weapon
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _explosionEffect;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private MeshRenderer _visual;
        [SerializeField] private float _startLifetime = 4f;

        private readonly float _effectTime = 2f;

        private ProjectileSettings _settings;
        private Coroutine _disableAfterEffect;
        private float _lifetime;

        public event Action<Projectile> Collided;
        public event Action<Projectile> Wasted;

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
            var damager = new Damager(_settings.AttackTargets);
            Unit unit = null;

            if (
                other.TryGetComponent(out Ground _)
                || (other.TryGetComponent(out unit) && damager.IsInLayerMask(unit.gameObject))
                )
            {
                DealDamage(damager, unit);
            }
            else
            {
                return;
            }

            if (_explosionEffect != null)
                _explosionEffect.gameObject.SetActive(true);

            _rigidbody.velocity = Vector3.zero;
            _visual.enabled = false;

            if (_disableAfterEffect != null)
                StopCoroutine(_disableAfterEffect);

            _disableAfterEffect = StartCoroutine(DisableAfterEffect());
        }

        public void Initialize(ProjectileSettings settings)
        {
            _settings = settings;
            _lifetime = _startLifetime;
            _rigidbody.velocity = VelocityCalculator.CalculateVelocity(settings.VelocityType, settings.ShotDirection);
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

        private void DealDamage(Damager damager, Unit unit)
        {
            switch (_settings.DamageType)
            {
                case DamageType.Direct:
                    DealDirectDamage(damager, unit);
                    break;
                case DamageType.Explosion:
                    DealExplosionDamageTargets(damager, unit);
                    break;
            }
        }

        private void DealDirectDamage(Damager damager, Unit unit)
        {
            if (unit != null)
                damager.ApplyDamage(unit, transform.position, _settings.Damage);
        }

        private void DealExplosionDamageTargets(Damager damager, Unit unit)
        {
            if (unit != null)
                damager.ApplyDamage(unit, transform.position, _settings.Damage);

            Collider[] targets = Physics.OverlapSphere(transform.position, _settings.DamageRadius);

            foreach (Collider target in targets)
            {
                if (target.TryGetComponent(out unit) == false)
                    continue;

                if (damager.IsInLayerMask(unit.gameObject) == false)
                    continue;

                Vector3 hitPoint = target.ClosestPoint(transform.position);
                damager.ApplyDamage(unit, hitPoint, _settings.Damage);
            }
        }
    }
}