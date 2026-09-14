using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battler.BattleSystem.Units.Actions.Weapon
{
    [CreateAssetMenu(menuName = "Projectile/DirectHitContactBehaviour")]
    public class DirectHitContactBehaviour : ContactBehaviour
    {
        public override void Activate(Collider collider, int damage, Vector3 hitPoint, LayerMask layerMask)
        {
            Damager damager = new();

            if (collider.gameObject.TryGetComponent(out Unit unit))
                if (Damager.IsInLayerMask(unit.gameObject, layerMask))
                    damager.ApplyDamage(unit, hitPoint, damage);
        }
    }
}
