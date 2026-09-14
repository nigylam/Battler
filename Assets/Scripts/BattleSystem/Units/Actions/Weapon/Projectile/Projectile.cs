using System;
using System.Collections;
using UnityEngine;

namespace Battler.BattleSystem.Units.Actions.Weapon
{
    public class Projectile : MonoBehaviour
    {
        [Serializable]
        public struct Settings
        {
            public float Lifetime;
            public VelocityType VelocityType;
            public ContactBehaviour ContactBehaviour;
        }

        [SerializeField] private ParticleSystem _explosionEffect;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private MeshRenderer _visual;

        private readonly float _effectTime = 2f;

        private Settings _settings;
        private LayerMask _layerMask;
        private int _damage;
        private float _remainingLifetime;
        private Coroutine _disableAfterEffect;

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
            _remainingLifetime -= Time.deltaTime;

            if (_remainingLifetime < 0)
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
            if (
                other.TryGetComponent(out Ground _) 
                || other.TryGetComponent(out Unit unit) && Damager.IsInLayerMask(unit.gameObject, _layerMask))
                _settings.ContactBehaviour.Activate(other, _damage, transform.position, _layerMask);
            else
                return;

            if (_explosionEffect != null)
                _explosionEffect.gameObject.SetActive(true);

            _rigidbody.velocity = Vector3.zero;
            _visual.enabled = false;

            if (_disableAfterEffect != null)
                StopCoroutine(_disableAfterEffect);

            _disableAfterEffect = StartCoroutine(DisableAfterEffect());
        }

        public void Initialize(Settings settings, Vector3 direction, int damage, LayerMask layerMask)
        {
            _layerMask = layerMask;
            _settings = settings;
            _remainingLifetime = settings.Lifetime;
            _damage = damage;
            _rigidbody.velocity = VelocityCalculator.CalculateVelocity(settings.VelocityType, direction);
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