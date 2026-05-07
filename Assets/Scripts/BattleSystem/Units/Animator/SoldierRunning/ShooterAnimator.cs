using Battler.BattleSystem.Units.Animation.SoldierRunning;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battler.BattleSystem.Units.Animation
{
    public class ShooterAnimator : SoldierAnimator
    {
        private readonly int AnimatorAttack = Animator.StringToHash("CanAttack");

        public override void OnAttack() { }

        public override void OnWentTarget()
        {
            SetBool(AnimatorAttack, true);
        }

        public override void OnLeaveTarget()
        {
            SetBool(AnimatorAttack, false);
        }
    }
}
