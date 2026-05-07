using UnityEngine;

namespace Battler.BattleSystem.Units.Attack
{
    public class ProjectileAttacker : Attacker
    {
        [SerializeField] private ProjectileSpawner _projectileSpawner;
        [SerializeField] private Transform _muzzlePoint;

        private LayerMask _attackTargets;
        private bool _isUpgraded;

        private void OnDisable()
        {
            _isUpgraded = false;
            Disable();
        }

        protected virtual void Disable() { }

        public override void Initialize(LayerMask attackTargets)
        {
            _attackTargets = attackTargets;
        }

        public override void Upgrade()
        {
            _isUpgraded = true;
        }

        protected override void Attack()
        {
            base.Attack();
            _projectileSpawner.Spawn(_muzzlePoint.position, _attackTargets, GetDirectionToTarget(_muzzlePoint.position), _isUpgraded);
        }
    }
}