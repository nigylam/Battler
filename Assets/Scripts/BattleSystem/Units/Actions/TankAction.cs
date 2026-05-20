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

        [Header("For testing")]
        [SerializeField] private Unit _target;
        [SerializeField] private LayerMask _targetLayer;

        private bool _isUpgraded;

        public override bool IsSingleAction => true;

        [ContextMenu("attadk")]
        public void Shoot()
        {
            ProcessAction(_target, () => _target != null).Forget();
        }

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
            await _visual.RotateTower(target.transform, ActionCancelToken);

            while (target.IsDead == false && isClose())
            {
                await UniTask.WaitUntil(() => IsCooldownEnded, cancellationToken: ActionCancelToken);
                _weapon.Shoot(_muzzlePoint.position, TargetLayer, _muzzlePoint.forward, _isUpgraded);
                StartCoolDown().Forget();
            }
        }
    }
}
