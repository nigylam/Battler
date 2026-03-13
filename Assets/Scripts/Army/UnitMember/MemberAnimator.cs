using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MemberAnimator : MonoBehaviour
{
    private readonly int AnimatorIsMoving = Animator.StringToHash("IsMoving");
    private readonly int AnimatorAttack = Animator.StringToHash("Attack");
    private readonly int AnimatorWin = Animator.StringToHash("Win");
    private readonly int AnimatorHit = Animator.StringToHash("Hit");
    private readonly int AnimatorDeath = Animator.StringToHash("Death");

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void ProcessMovingAnimations(bool isMoving)
    {
        _animator.SetBool(AnimatorIsMoving, isMoving);
    }

    public void OnAttack()
    {
        _animator.SetTrigger(AnimatorAttack);
    }

    public void OnHit()
    {
        //_animator.SetTrigger(AnimatorHit);
    }

    public void OnWin()
    {
        _animator.SetTrigger(AnimatorWin);
    }

    public void OnDeath()
    {
        _animator.SetTrigger(AnimatorDeath);
    }
}
