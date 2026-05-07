using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battler.BattleSystem.Units.Animation.SoldierRunning
{
    public class ShootAnimationEventSender : MonoBehaviour
    {
        public event Action ReadyShoot;

        public void OnReadyShoot()
        {
            ReadyShoot?.Invoke();
        }
    }
}
