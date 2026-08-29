using UnityEngine;

namespace Battler.BattleSystem.Units
{
    public class UnitStats : MonoBehaviour
    {
        [SerializeField] private int _actionValue;
        [SerializeField] private int _actionValueUpgraded;
        [SerializeField] private int _health;
        [SerializeField] private int _healthUpgraded;

        public int ActionValue => _actionValue;
        public int ActionValueUpgraded => _actionValueUpgraded;
        public int Health => _health;
        public int HealthUpgraded => _healthUpgraded;
    }
}
