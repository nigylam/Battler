using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Battler.BattleSystem.Units.Visual
{
    public class ChopperVisual : UnitVisual
    {
        [SerializeField] private Transform _propeller;
        [SerializeField] private float _propellerRotationSpeed;
        [SerializeField] private float _shakeOffset;
        [SerializeField] private float _shakeSpeed = 2f;

        [Header("Explosion")]
        [SerializeField] private ParticleSystem _explosionEffect;
        [SerializeField] private ParticleSystem _smokeEffect;
        [SerializeField] private Material _explodedMaterial;
        [SerializeField] private float _destroyEffectDuration;

        private Vector3 _startPosition;

        private void Start()
        {
            _startPosition = transform.position;
        }

        private void Update()
        {
            float rotationSpeed = _propellerRotationSpeed * Time.deltaTime;
            _propeller.Rotate(0, rotationSpeed, 0);
            Shake();
        }

        public override void PlayDeathAnimation()
        {
            SetMaterial(_explodedMaterial);
            _explosionEffect.gameObject.SetActive(true);
            _smokeEffect.gameObject.SetActive(true);
            DisableAfterDelay().Forget();
        }

        private async UniTask DisableAfterDelay()
        {
            await UniTask.Delay(System.TimeSpan.FromSeconds(_destroyEffectDuration), cancellationToken: destroyCancellationToken);
            OnDeathAnimationPlayed();
        }

        private void Shake()
        {
            float newY = _startPosition.y + Mathf.Sin(Time.time * _shakeSpeed) * _shakeOffset;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }
}
