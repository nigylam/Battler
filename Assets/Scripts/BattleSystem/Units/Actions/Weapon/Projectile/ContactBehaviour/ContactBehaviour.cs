using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battler.BattleSystem.Units.Actions.Weapon
{
    public abstract class ContactBehaviour : ScriptableObject
    {
        public abstract void Activate(Collider collider, int damage, Vector3 hitPoint, LayerMask layerMask);
    }
}
