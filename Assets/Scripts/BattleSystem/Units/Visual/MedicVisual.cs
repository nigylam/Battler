using UnityEngine;

namespace Battler
{
    public class MedicVisual : UnitVisual
    {
        private readonly int AnimatorHeal = Animator.StringToHash("Heal");

        public void PlayHealAnimation()
        {
            SetTrigger(AnimatorHeal);
        }
    }
}
