using Battler.BattleSystem.Units;
using System;
using UnityEngine;

namespace Battler.BattleSystem.Units.Actions.Weapon
{
    public class MeleeWeapon : MonoBehaviour
    {
        [SerializeField] private Collider _collider;

        private Damager _damager;
        private int _damage;
        private int _damageUpgraded;
        private int _currentDamage;
        private bool _damageDid = false;

        public event Action DamageDid;

        private void OnEnable()
        {
            _collider.enabled = false;
        }

        public void Initialize(LayerMask targetLayer, int actionValue, int actionValueUpgraded)
        {
            _damage = actionValue;
            _damageUpgraded = actionValueUpgraded;
            _currentDamage = _damage;
            _damager = new Damager(targetLayer);
        }

        public void Upgrade()
        {
            _currentDamage = _damageUpgraded;
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

            if (_damager.IsInLayerMask(other.gameObject) == false)
                return;

            if (other.TryGetComponent(out Unit member))
            {
                if (_damageDid)
                    return;

                Vector3 hitPoint = other.ClosestPoint(transform.position);
                _damageDid = true;
                _damager.ApplyDamage(member, hitPoint, _currentDamage);
                DamageDid?.Invoke();
            }
        }
    }
}
