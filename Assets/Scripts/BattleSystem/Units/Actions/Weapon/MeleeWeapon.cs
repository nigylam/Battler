using Battler.BattleSystem.Units;
using System;
using UnityEngine;

namespace Battler.BattleSystem.Units.Actions.Weapon
{
    public class MeleeWeapon : Damager
    {
        [SerializeField] private Collider _collider;

        private bool _damageDid = false;

        public event Action DamageDid;

        private void OnEnable()
        {
            _collider.enabled = false;
        }

        public void EnableDamage()
        {
            _collider.enabled = true;
            _damageDid = false;
        }

        public void DisableDamage()
        {
            _collider.enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_collider.enabled == false)
                return;

            if (IsInLayerMask(other.gameObject) == false)
                return;

            if (other.TryGetComponent(out Unit member))
            {
                if (_damageDid)
                    return;

                Vector3 hitPoint = other.ClosestPoint(transform.position);
                _damageDid = true;
                ApplyDamage(member, hitPoint);
                DamageDid?.Invoke();
            }
        }
    }
}
