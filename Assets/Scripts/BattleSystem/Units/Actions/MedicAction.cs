using Battler.BattleSystem.Units;
using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Battler.BattleSystem.Units.Actions
{
    public class MedicAction : UnitAction
    {
        [SerializeField] private int _healPerTime;
        [SerializeField] private int _healRange;
        [SerializeField] private int _healPerTimeUpgraded;
        [SerializeField] private MedicVisual _visual;

        private CancellationTokenSource _actionCts;

        private void OnDisable()
        {
            StopAction();
        }

        public override void Upgrade()
        {
            _healPerTime = _healPerTimeUpgraded;
        }

        public override void StartAction(Unit target)
        {
            base.StartAction(target);
            _actionCts?.Cancel();
            _actionCts = new CancellationTokenSource();
            ExecuteSequenceAsync(target, _actionCts.Token).Forget();
        }

        public override void StopAction()
        {
            _actionCts?.Cancel();
        }

        private void Heal()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, _healRange);

            foreach (Collider collider in colliders)
                if (collider.TryGetComponent(out Unit unit))
                    if (IsInLayerMask(unit.gameObject) == false)
                        unit.Heal(_healPerTime);
        }

        private async UniTask ExecuteSequenceAsync(Unit target, CancellationToken token)
        {
            await UniTask.Delay(System.TimeSpan.FromSeconds(GetCooldownTime()), cancellationToken: token);

            while (target != null && token.IsCancellationRequested == false)
            {
                if (_visual != null)
                    _visual.PlayHealAnimation();

                Heal();
                await UniTask.Delay(System.TimeSpan.FromSeconds(GetCooldownTime()), cancellationToken: token);
            }
        }

        private bool IsInLayerMask(GameObject obj)
        {
            return (TargetLayer.value & 1 << obj.layer) != 0;
        }
    }
}
