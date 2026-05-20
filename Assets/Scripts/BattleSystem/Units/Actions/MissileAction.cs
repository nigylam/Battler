using Battler.BattleSystem.Units.Actions.Weapon;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace Battler.BattleSystem.Units.Actions
{
    public class MissileAction : UnitAction
    {
        [SerializeField] private ProjectileWeapon _weapon;
        [SerializeField] private Transform[] _muzzlePoints;

        private readonly float _shotDelay = 0.5f;
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
            while (target.IsDead == false && isClose())
            {
                await UniTask.WaitUntil(() => IsCooldownEnded, cancellationToken: ActionCancelToken);

                if (target.IsDead)
                    return;

                foreach(var muzzlePoint in _muzzlePoints)
                {
                    _weapon.Shoot(muzzlePoint.position, TargetLayer, target.transform.position - muzzlePoint.position, _isUpgraded);
                    await UniTask.WaitForSeconds(_shotDelay, cancellationToken: ActionCancelToken);
                }
                
                StartCoolDown().Forget();
            }
        }
    }
}
