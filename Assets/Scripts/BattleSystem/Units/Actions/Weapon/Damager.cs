using Battler.BattleSystem.Units;
using UnityEngine;

namespace Battler.BattleSystem.Units.Actions.Weapon
{
    public abstract class Damager : MonoBehaviour
    {
        [SerializeField] private int _damage;
        [SerializeField] private int _damageUpgraded;

        private int _damageOffset = 2;
        private int _damageMinValue = 1;
        private LayerMask _attackTargets;

        public virtual void Initialize(LayerMask attackTargets)
        {
            _attackTargets = attackTargets;
        }

        public virtual void Upgrade()
        {
            _damage = _damageUpgraded;
        }

        protected void ApplyDamage(Unit member, Vector3 hitPoint)
        {
            int damage = Random.Range(_damage - _damageOffset, _damage + _damageOffset);
            damage = Mathf.Clamp(damage, _damageMinValue, int.MaxValue);
            member.TakeDamage(damage, hitPoint);
        }

        protected bool IsInLayerMask(GameObject obj)
        {
            return (_attackTargets.value & 1 << obj.layer) != 0;
        }
    }
}