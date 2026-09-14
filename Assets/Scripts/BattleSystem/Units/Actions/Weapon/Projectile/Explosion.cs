using System;
using UnityEngine;

namespace Battler.BattleSystem.Units.Actions.Weapon
{
    public class Explosion
    {
        [Serializable]
        public struct Settings
        {
            public float Radius;
        }

        private const int CollidersArraySize = 10;

        private readonly Settings _settings;
        private readonly int _damage;

        public Explosion(Settings settings, int damage)
        {
            _settings = settings;
            _damage = damage;
        }

        public void Activate(Vector3 position, LayerMask layerMask)
        {
            Collider[] colliders = new Collider[CollidersArraySize];
            Physics.OverlapSphereNonAlloc(position, _settings.Radius, colliders, layerMask);
            var damager = new Damager(layerMask);

            foreach (Collider collider in colliders)
            {
                if (collider == null) 
                    continue;

                if (collider.TryGetComponent(out Unit unit))
                    damager.ApplyDamage(unit, collider.ClosestPoint(position), _damage);
            }
        }
    }
}
