using System;
using UnityEngine;

namespace Battler.BattleSystem.Units.Actions.Weapon
{
    public class ProjectileDamageDealer
    {
        public static bool TryDealDamage(DamageType damageType, Collider hitCollider, Transform transform, float damageRadius,
            Func<GameObject, bool> isInLayerMask, Action<Unit, Vector3> applyDamage)
        {
            if (hitCollider.TryGetComponent(out Unit unit) == false)
                return false;

            if (isInLayerMask(unit.gameObject) == false)
                return false;

            applyDamage(unit, transform.position);

            switch (damageType)
            {
                case DamageType.Direct:
                    break;
                case DamageType.Explosion:
                    DealExplosionDamageTargets(transform, damageRadius, isInLayerMask, applyDamage);
                    break;
            }

            return true;
        }

        private static void DealExplosionDamageTargets(Transform transform, float damageRadius,
            Func<GameObject, bool> isInLayerMask, Action<Unit, Vector3> applyDamage)
        {
            if (damageRadius <= 1)
                throw new ArgumentOutOfRangeException(nameof(damageRadius));

            Collider[] targets = Physics.OverlapSphere(transform.position, damageRadius);

            foreach (Collider target in targets)
            {
                if (target.TryGetComponent(out Unit unit) == false)
                    continue;

                if (isInLayerMask(unit.gameObject) == false)
                    continue;

                Vector3 hitPoint = target.ClosestPoint(transform.position);
                applyDamage(unit, hitPoint);
            }
        }
    }
}
