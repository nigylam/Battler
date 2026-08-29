using Battler.BattleSystem.Units.Visual;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace Battler.BattleSystem.Units.Actions
{
    public class MedicAction : UnitAction
    {
        [SerializeField] private MedicVisual _visual;
        [SerializeField] private Sound _sound;

        public override bool IsSingleAction => true;

        protected override void Disable()
        {
            Stop();
        }

        protected override async UniTask ProcessAction(Unit target, Func<bool> isClose)
        {
            if (target.IsDead || isClose() == false || target.HealthDecreased == false)
                return;

            _visual.OnHealing();
            await UniTask.WaitUntil(() => IsCooldownEnded, cancellationToken: ActionCancelToken);
            Heal(target);
            _sound.PlayActionSound();
            StartCoolDown().Forget();
        }

        private void Heal(Unit target)
        {
            target.Heal(CurrentActionValue);
        }
    }
}
