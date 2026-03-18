using System;
using UnityEngine;

public abstract class UnitAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private readonly int AnimatorWin = Animator.StringToHash("Win");
    private readonly int AnimatorHit = Animator.StringToHash("Hit");
    private readonly int AnimatorDeath = Animator.StringToHash("Death");
    private readonly string AnimatorHitLayer = "HitLayer";

    private int _hitLayer;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _hitLayer = _animator.GetLayerIndex(AnimatorHitLayer);
    }

    public void OnHit()
    {
        SetTrigger(AnimatorHit);
    }

    public void OnWin()
    {
        SetTrigger(AnimatorWin);
    }

    public void OnDeath()
    {
        _animator.SetLayerWeight(_hitLayer, 0);
        _animator.SetTrigger(AnimatorDeath);
    }

    protected void SetBool(int id, bool value)
    {
        _animator.SetBool(id, value);
    }

    protected void SetTrigger(int id) 
    {
        _animator.SetTrigger(id);
    }
}
