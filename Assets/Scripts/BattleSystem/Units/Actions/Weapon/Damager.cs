using UnityEngine;

namespace Battler.BattleSystem.Units.Actions.Weapon
{
    public class Damager
    {
        private readonly int _damageOffset = 2;
        private readonly int _damageMinValue = 1;
        private readonly LayerMask _attackTargets;

        public Damager(LayerMask attackTargets)
        {
            _attackTargets = attackTargets;
        }

        public void ApplyDamage(Unit member, Vector3 hitPoint, int damage)
        {
            int finalDamage = Random.Range(damage - _damageOffset, damage + _damageOffset);
            finalDamage = Mathf.Clamp(finalDamage, _damageMinValue, int.MaxValue);
            member.TakeDamage(finalDamage, hitPoint);
        }

        public bool IsInLayerMask(GameObject obj)
        {
            return (_attackTargets.value & 1 << obj.layer) != 0;
        }
    }
}