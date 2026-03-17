using UnityEngine;

public class SoldierAnimator : MemberAnimator
{
    private readonly int AnimatorIsMoving = Animator.StringToHash("IsMoving");
    private readonly int AnimatorAttack = Animator.StringToHash("Attack");

    public void ProcessMovingAnimations(bool isMoving)
    {
        SetBool(AnimatorIsMoving, isMoving);
    }

    public void OnAttack()
    {
        SetTrigger(AnimatorAttack);
    }
}
