using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace Battler.BattleSystem.Units
{
    public class EffectSpawner : Spawner<ParticleSystem>
    {
        private readonly float _lifetime = 1f;
        private CancellationTokenSource _actionCts;

        private void OnDisable()
        {
            _actionCts.Cancel();
        }

        public ParticleSystem Spawn(Vector3 position)
        {
            ParticleSystem effect = Pool.Get();
            effect.transform.position = position;
            effect.transform.SetParent(transform);
            TryAddToActiveList(effect);
            ReleaseAfterDelayAsync(effect).Forget();
            return effect;
        }

        public override void Restart()
        {
            _actionCts.Cancel();
            base.Restart();
        }

        protected override void OnAwake()
        {
            _actionCts = new CancellationTokenSource();
        }

        private async UniTask ReleaseAfterDelayAsync(ParticleSystem effect)
        {
            if (await UniTask.Delay(System.TimeSpan.FromSeconds(_lifetime), cancellationToken: _actionCts.Token).SuppressCancellationThrow())
                return;

            if (effect != null)
                Release(effect);
        }

        protected override void Release(ParticleSystem item)
        {
            if (TryRemoveFromActiveList(item) == false)
                return;

            Pool.Release(item);
        }
    }
}
