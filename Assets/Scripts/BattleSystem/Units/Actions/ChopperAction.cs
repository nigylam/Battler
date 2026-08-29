using Battler.BattleSystem.Units.Actions.Weapon;
using Battler.BattleSystem.Units.Visual;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace Battler.BattleSystem.Units.Actions
{
    public class ChopperAction : UnitAction
    {
        [SerializeField] private ProjectileWeapon _weapon;
        [SerializeField] private Transform[] _muzzlePoints;
        [SerializeField] private TankVisual _visual;

        private readonly float _fireRate = 0.1f;
        private readonly int _fireBurstSize = 8;
        private readonly float _targetOffsetY = 1f;

        public override bool IsSingleAction => true;

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

                for (int i = 0; i < _fireBurstSize; i++)
                {
                    foreach (var muzzlePoint in _muzzlePoints)
                    {
                        _weapon.Shoot(muzzlePoint.position, TargetLayer, GetDirection(target.transform.position, muzzlePoint.position), CurrentActionValue);
                        await UniTask.WaitForSeconds(_fireRate, cancellationToken:ActionCancelToken);
                    }
                }

                StartCoolDown().Forget();
            }
        }

        private Vector3 GetDirection(Vector3 targetPosition, Vector3 startPosition)
        {
            return new Vector3(targetPosition.x, targetPosition.y + _targetOffsetY, targetPosition.z) - startPosition;
        }
    }
}
