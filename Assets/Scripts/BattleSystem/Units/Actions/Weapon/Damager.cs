using UnityEngine;

namespace Battler.BattleSystem.Units.Actions.Weapon
{
    public class Damager
    {
        private readonly int _damageOffset = 2;
        private readonly int _damageMinValue = 1;

        private readonly LayerMask _layerMask;

        public Damager(LayerMask layerMask)
        {
            _layerMask = layerMask;
        }

        public Damager() { }

        public void ApplyDamage(Unit member, Vector3 hitPoint, int damage)
        {
            int finalDamage = Random.Range(damage - _damageOffset, damage + _damageOffset);
            finalDamage = Mathf.Clamp(finalDamage, _damageMinValue, int.MaxValue);
            member.TakeDamage(finalDamage, hitPoint);
        }

        public bool IsInLayerMask(GameObject obj)
        {
            return IsInLayerMask(obj, _layerMask);
        }

        public static bool IsInLayerMask(GameObject obj, LayerMask layerMask)
        {
            return (layerMask.value & 1 << obj.layer) != 0;
        }
    }
}