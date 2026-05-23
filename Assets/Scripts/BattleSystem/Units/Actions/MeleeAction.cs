using Battler.BattleSystem.Units;
using Battler.BattleSystem.Units.Actions.Weapon;
using Battler.BattleSystem.Units.Visual;
using Cysharp.Threading.Tasks;
using UnityEngine;
using System;

namespace Battler.BattleSystem.Units.Actions
{
    public class MeleeAction : UnitAction
    {
        [SerializeField] private MeleeVisual _visual;
        [SerializeField] private MeleeWeapon _weapon;

        private void OnEnable()
        {
            _visual.AttackStarted += EnableDamage;
            _visual.AttackEnded += DisableDamage;
        }

        public override void Initialize(LayerMask targetLayer)
        {
            base.Initialize(targetLayer);
            _weapon.Initialize(targetLayer);
        }

        public override void Upgrade()
        {
            _weapon.Upgrade();
        }

        protected override void Disable()
        {
            _visual.AttackStarted -= EnableDamage;
            _visual.AttackEnded -= DisableDamage;
        }

        protected override async UniTask ProcessAction(Unit target, Func<bool> isClose)
        {
            while (target.IsDead == false && isClose())
            {
                await UniTask.WaitUntil(() => IsCooldownEnded, cancellationToken: ActionCancelToken);

                if (target.IsDead)
                    return;

                _visual.PlayAttackAnimation();
                StartCoolDown().Forget();
            }
        }

        private void EnableDamage()
        {
            _weapon.EnableDamage();
        }

        private void DisableDamage()
        {
            _weapon.DisableDamage();
        }
    }
}
