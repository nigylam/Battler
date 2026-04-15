using UnityEngine;

public class Mortar : Unit
{
    [SerializeField] private MortarAnimator _animator;

    protected override UnitAnimator Animator => _animator;
}
