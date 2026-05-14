using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battler.BattleSystem.Units.Animation.SoldierRunning
{
    public class MeleeAnimator : SoldierAnimator
    {
        private readonly int AnimatorAttack = Animator.StringToHash("Attack");

        public override void OnAttack()
        {
            SetTrigger(AnimatorAttack);
        }
    }
}
