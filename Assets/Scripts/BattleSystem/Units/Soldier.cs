using UnityEngine;

public class Soldier : Unit
{
    [SerializeField] private SoldierAnimator _animator;
    protected override UnitAnimator Animator => _animator;

    private void Update()
    {
        _animator.ProcessMovingAnimations(IsMoving);
    }

    protected override void OnAttackStarted()
    {
        _animator.OnAttack();
    }
}
