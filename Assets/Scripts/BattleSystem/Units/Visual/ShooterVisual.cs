using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battler
{
    public class ShooterVisual : UnitVisual
    {
        private readonly int AnimatorShot = Animator.StringToHash("Shot");

        public void PlayShotAnimation()
        {
            SetTrigger(AnimatorShot);
        }
    }
}
