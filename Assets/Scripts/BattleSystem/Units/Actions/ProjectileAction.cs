using Battler.BattleSystem.Units.Actions.Weapon;
using Battler.BattleSystem.Units.Visual;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace Battler.BattleSystem.Units.Actions
{
    public class ProjectileAction : UnitAction
    {
        [SerializeField] private ShooterVisual _visual;
        [SerializeField] private ProjectileWeapon _weapon;
        [SerializeField] private Transform _muzzlePoint;

        private bool _isUpgraded;

        public override void Upgrade()
        {
            _isUpgraded = true;
        }

        protected override void Disable()
        {
            Stop();
        }

        protected override async UniTask ProcessAction(Unit target, Func<bool> isClose)
        {
            StartCoolDown().Forget();

            while (target.IsDead == false && isClose())
            {
                await UniTask.WaitUntil(() => IsCooldownEnded, cancellationToken: ActionCancelToken);

                if (_visual != null)
                    _visual.PlayShotAnimation();

                _weapon.Spawn(_muzzlePoint.position, TargetLayer, GetDirectionToTarget(target.transform.position, _muzzlePoint.position), _isUpgraded);
                StartCoolDown().Forget();
            }
        }
    }
}
