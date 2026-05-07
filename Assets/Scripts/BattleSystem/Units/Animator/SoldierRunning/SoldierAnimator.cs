using UnityEngine;

namespace Battler.BattleSystem.Units.Animation.SoldierRunning
{
    public abstract class SoldierAnimator : UnitAnimator
    {
        private readonly int AnimatorIsMoving = Animator.StringToHash("IsMoving");

        public void ProcessMovingAnimations(bool isMoving)
        {
            SetBool(AnimatorIsMoving, isMoving);
        }

        public abstract void OnAttack();
    }
}