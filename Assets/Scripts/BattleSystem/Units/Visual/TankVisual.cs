using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace Battler.BattleSystem.Units.Visual
{
    public class TankVisual : UnitVisual
    {
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _towerRotationOffsetY;
        [SerializeField] private float _destroyEffectDuration;
        [SerializeField] private GameObject _tower;
        [SerializeField] private ParticleSystem _explosionEffect;
        [SerializeField] private ParticleSystem _smokeEffect;
        [SerializeField] private Material _explodedMaterial;

        [Header("For testing")]
        [SerializeField] private Transform _target;

        [ContextMenu("rotate")]
        public void Rotate()
        {
            RotateTower(_target, destroyCancellationToken).Forget();
        }

        public async UniTask RotateTower(Transform target, CancellationToken token)
        {
            Vector3 targetPosition = target.position;
            targetPosition.y -= _towerRotationOffsetY;
            Quaternion targetRotation = Quaternion.LookRotation(targetPosition - transform.position);

            while (_tower.transform.rotation != targetRotation)
            {
                float step = _rotationSpeed * Time.deltaTime;
                _tower.transform.rotation = Quaternion.RotateTowards(_tower.transform.rotation, targetRotation, step);
                await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken: token);
            }
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
    }
}
