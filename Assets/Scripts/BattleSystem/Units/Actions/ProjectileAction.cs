using Battler.BattleSystem.Units.Actions.Weapon;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace Battler.BattleSystem.Units.Actions
{
    public class ProjectileAction : UnitAction
    {
        [SerializeField] private ShooterVisual _visual;
        [SerializeField] private ProjectileWeapon _weapon;
        [SerializeField] private Transform _muzzlePoint;

        private bool _isUpgraded;
        private CancellationTokenSource _actionCts;

        private void OnDisable()
        {
            StopAction();
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

        public override void Upgrade()
        {
            _isUpgraded = true;
        }

        private async UniTask ExecuteSequenceAsync(Unit target, CancellationToken token)
        {
            await UniTask.Delay(System.TimeSpan.FromSeconds(GetCooldownTime()), cancellationToken: token);

            while (target != null && token.IsCancellationRequested == false)
            {
                if (_visual != null)
                    _visual.PlayShotAnimation();

                _weapon.Spawn(_muzzlePoint.position, TargetLayer, GetDirectionToTarget(_muzzlePoint.position), _isUpgraded);
                await UniTask.Delay(System.TimeSpan.FromSeconds(GetCooldownTime()), cancellationToken: token);
            }
        }
    }
}
