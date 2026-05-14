using System;
using UnityEngine;

namespace Battler.BattleSystem.Units.Animation.SoldierRunning
{
    public class MeleeWeaponAnimationEventSender : MonoBehaviour
    {
        public event Action AttackHitEnable;
        public event Action AttackHitDisable;

        public void OnAttackHitEnable()
        {
            AttackHitEnable?.Invoke();
        }

        public void OnAttackHitDisable()
        {
            AttackHitDisable?.Invoke();
        }
    }
}