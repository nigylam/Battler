using Battler.BattleSystem.Units.Actions.Weapon;
using Battler.BattleSystem.Units.Visual;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace Battler.BattleSystem.Units.Actions
{
    public class ShootAction : UnitAction
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

                _weapon.Shoot(_muzzlePoint.position, TargetLayer, target.Body.position - _muzzlePoint.position, _isUpgraded);
                StartCoolDown().Forget();
            }
        }

        private Vector3 GetDirection(Vector3 targetPosition, Vector3 startPosition)
        {
            Vector3 shotDirection = targetPosition - startPosition;
            shotDirection.y = 0;
            return shotDirection;
        }
    }
}
