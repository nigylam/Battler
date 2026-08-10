using Battler.BattleSystem.Units.Visual;
using Cysharp.Threading.Tasks;
using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Battler.BattleSystem.Units.Actions
{
    public class MedicAction : UnitAction
    {
        [SerializeField] private int _healPerTime;
        [SerializeField] private int _healPerTimeUpgraded;
        [SerializeField] private MedicVisual _visual;
        [SerializeField] private Sound _sound;

        public override bool IsSingleAction => true;

        public override void Upgrade()
        {
            _healPerTime = _healPerTimeUpgraded;
        }

        protected override void Disable()
        {
            Stop();
        }

        protected override async UniTask ProcessAction(Unit target, Func<bool> isClose)
        {
            if (target.IsDead || isClose() == false)
                return;

            await UniTask.WaitUntil(() => IsCooldownEnded, cancellationToken: ActionCancelToken);
            _visual.OnHealing();
            Heal(target);
            _sound.PlayActionSound();
            StartCoolDown().Forget();
        }

        private void Heal(Unit target)
        {
            target.Heal(_healPerTime);
        }
    }
}
