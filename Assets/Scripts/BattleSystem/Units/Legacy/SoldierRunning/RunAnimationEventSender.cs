using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battler.BattleSystem.Units.Animation.SoldierRunning
{
    public class RunAnimationEventSender : MonoBehaviour
    {
        public event Action RunStarted;

        public void OnRunStarted()
        {
            RunStarted?.Invoke();
        }
    }
}
