using System;
using UnityEngine;

namespace Battler.BattleSystem.Units.Animation
{
    public class DeathAnimationEventSender : MonoBehaviour
    {
        public event Action AnimationEnded;

        public void OnAnimationEnded()
        {
            AnimationEnded?.Invoke();
        }
    }
}