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
        [SerializeField] private Sound _sound;

        //For testing
        [ContextMenu("attack")]
        public override void Shoot()
        {
            _weapon.Initialize(_targetLayer);
            base.Shoot();
        }

        private void OnEnable()
        {
            _visual.AttackStarted += EnableDamage;
            _visual.AttackEnded += DisableDamage;
            _weapon.DamageDid += OnDamageDid;
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
            _weapon.DamageDid -= OnDamageDid;
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
            _sound.PlayActionSound();
            _weapon.EnableDamage();
        }

        private void DisableDamage()
        {
            _weapon.DisableDamage();
        }

        private void OnDamageDid()
        {
            _sound.PlayDamageSound();
        }
    }
}
