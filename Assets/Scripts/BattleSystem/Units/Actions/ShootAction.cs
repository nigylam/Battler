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

                if (target.IsDead == false)
                {
                    _weapon.Shoot(_muzzlePoint.position, TargetLayer, target.Body.position - _muzzlePoint.position, CurrentActionValue);
                    StartCoolDown().Forget();
                }
            }
        }
    }
}
