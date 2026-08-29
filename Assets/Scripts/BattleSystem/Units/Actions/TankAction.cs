using Battler.BattleSystem.Units.Actions.Weapon;
using Battler.BattleSystem.Units.Visual;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace Battler.BattleSystem.Units.Actions
{
    public class TankAction : UnitAction
    {
        [SerializeField] private TankVisual _visual;
        [SerializeField] private ProjectileWeapon _weapon;
        [SerializeField] private Transform _muzzlePoint;
        [SerializeField] private float _fireRate;
        [SerializeField] private int _fireBurstSize;

        public override bool IsSingleAction => true;

        protected override void Disable()
        {
            Stop();
        }

        protected override async UniTask ProcessAction(Unit target, Func<bool> isClose)
        {
            await _visual.RotateTower(target.Body, ActionCancelToken);
            await UniTask.WaitUntil(() => IsCooldownEnded, cancellationToken: ActionCancelToken);

            if (target.IsDead)
                return;

            for (int i = 0; i < _fireBurstSize; i++)
            {
                _weapon.Shoot(_muzzlePoint.position, TargetLayer, _muzzlePoint.forward, CurrentActionValue);
                await UniTask.WaitForSeconds(_fireRate, cancellationToken: ActionCancelToken);
            }

            StartCoolDown().Forget();
        }
    }
}
