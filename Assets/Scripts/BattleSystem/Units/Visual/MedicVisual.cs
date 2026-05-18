using System.Collections;
using UnityEngine;

namespace Battler.BattleSystem.Units.Visual
{
    public class MedicVisual : UnitVisual
    {
        private readonly int AnimatorHeal = Animator.StringToHash("Heal");

        public void OnHealing()
        {
            SetTrigger(AnimatorHeal);
        }
    }
}
