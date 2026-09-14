using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battler.BattleSystem.Units.Actions.Weapon
{
    [CreateAssetMenu(menuName = "Projectile/ExplodeContactBehaviour")]
    public class ExplodeContactBehaviour : ContactBehaviour
    {
        [SerializeField] private Explosion.Settings _explosionSettings;

        public override void Activate(Collider collider, int damage, Vector3 hitPoint, LayerMask layerMask)
        {
            new Explosion(_explosionSettings, damage).Activate(hitPoint, layerMask);
        }
    }
}
