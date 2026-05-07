using Battler.BattleSystem.Units.Animation;
using UnityEngine;

namespace Battler.BattleSystem.Units
{
    public class Mortar : Unit
    {
        [SerializeField] private MortarAnimator _animator;

        protected override UnitAnimator Animator => _animator;
    }
}