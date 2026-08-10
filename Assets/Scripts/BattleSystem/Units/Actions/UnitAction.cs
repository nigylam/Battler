using UnityEngine;
using System;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace Battler.BattleSystem.Units.Actions
{
    public abstract class UnitAction : MonoBehaviour
    {
        [SerializeField] private float _cooldownTime;

        private readonly float _coolDownOffset = 0.6f;

        private CancellationTokenSource _ñooldownCts;
        private CancellationTokenSource _actionCts;

        public virtual bool IsSingleAction => false;

        protected bool IsCooldownEnded { get; private set; } = true;
        protected CancellationToken ActionCancelToken => _actionCts.Token;
        protected LayerMask TargetLayer { get; private set; }


        [Header("For testing")]
        [SerializeField] private Unit _target;
        [SerializeField] public LayerMask _targetLayer;

        [ContextMenu("attadk")]
        public virtual void Shoot()
        {
            StartAction(_target, () => _target != null).Forget();
            TargetLayer = _targetLayer;
        }

        private void Awake()
        {
            _ñooldownCts = new CancellationTokenSource();
        }

        private void OnDisable()
        {
            _ñooldownCts.Cancel();
            _actionCts?.Cancel();
            Disable();
        }

        public virtual void Initialize(LayerMask targetLayer)
        {
            TargetLayer = targetLayer;
        }

        public async UniTask StartAction(Unit target, Func<bool> isClose)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target));

            _actionCts?.Cancel();
            _actionCts = new CancellationTokenSource();
            await ProcessAction(target, isClose);
        }

        public void Stop()
        {
            _actionCts?.Cancel();
        }

        public abstract void Upgrade();

        protected virtual void Disable() { }
        protected abstract UniTask ProcessAction(Unit target, Func<bool> isClose);

        protected async UniTask StartCoolDown()
        {
            IsCooldownEnded = false;
            await UniTask.Delay(TimeSpan.FromSeconds(GetCooldownTime()), cancellationToken: _ñooldownCts.Token);
            IsCooldownEnded = true;
        }

        private float GetCooldownTime()
        {
            return UnityEngine.Random.Range(_cooldownTime - _coolDownOffset, _cooldownTime + _coolDownOffset);
        }
    }
}
