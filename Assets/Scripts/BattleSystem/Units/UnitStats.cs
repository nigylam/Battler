using UnityEngine;

namespace Battler.BattleSystem.Units
{
    public class UnitStats : MonoBehaviour
    {
        [SerializeField] private int _actionValue;
        [SerializeField] private int _health;
        [SerializeField] private float _range;
        [SerializeField] private float _cooldown;

        public int ActionValue => _actionValue;
        public int Health => _health;
        public float Range => _range;
        public float Cooldown => _cooldown;
    }
}
