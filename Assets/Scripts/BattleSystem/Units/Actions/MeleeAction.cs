using Battler.BattleSystem.Units;
using Battler.BattleSystem.Units.Actions.Weapon;
using Battler.BattleSystem.Units.Visual;
using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Battler.BattleSystem.Units.Actions
{
    public class MeleeAction : UnitAction
    {
        [SerializeField] private MeleeVisual _visual;
        [SerializeField] private MeleeWeapon _weapon;

        private CancellationTokenSource _actionCts;

        private void OnEnable()
        {
            _visual.AttackStarted += EnableDamage;
            _visual.AttackEnded += DisableDamage;
        }

        private void OnDisable()
        {
            _visual.AttackStarted -= EnableDamage;
            _visual.AttackEnded -= DisableDamage;
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

        private async UniTask ExecuteSequenceAsync(Unit target, CancellationToken token)
        {
            while (target != null && token.IsCancellationRequested == false)
            {
                _visual.PlayAttackAnimation();
                await UniTask.Delay(System.TimeSpan.FromSeconds(GetCooldownTime()), cancellationToken: token);
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
