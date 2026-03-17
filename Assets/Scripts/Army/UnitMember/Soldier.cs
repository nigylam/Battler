
public class Soldier : UnitMember
{
    private SoldierAnimator _animator;
    protected override MemberAnimator Animator => _animator;

    protected override void Awake()
    {
        base.Awake();
        _animator = GetComponent<SoldierAnimator>();
    }

    private void Update()
    {
        _animator.ProcessMovingAnimations(IsMoving);
    }

    protected override void OnAttackStarted()
    {
        _animator.OnAttack();
    }
}
