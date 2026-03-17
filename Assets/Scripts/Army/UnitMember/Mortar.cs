
public class Mortar : UnitMember
{
    private MortarAnimator _animator;

    protected override MemberAnimator Animator => _animator;

    protected override void Awake()
    {
        base.Awake();
        _animator = GetComponent<MortarAnimator>();
    }
}
