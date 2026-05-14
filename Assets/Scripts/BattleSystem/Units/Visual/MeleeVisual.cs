using System;
using UnityEngine;

namespace Battler
{
    public class MeleeVisual : UnitVisual
    {
        private readonly int AnimatorAttack = Animator.StringToHash("Attack");

        public event Action AttackStarted;
        public event Action AttackEnded;

        public void PlayAttackAnimation()
        {
            SetTrigger(AnimatorAttack);
        }

        public void OnAttackActionStarted()
        {
            AttackStarted?.Invoke();
        }

        public void OnAttackActionEnded()
        {
            AttackEnded?.Invoke();
        }
    }
}