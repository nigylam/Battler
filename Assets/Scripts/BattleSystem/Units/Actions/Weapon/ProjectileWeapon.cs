using UnityEngine;

namespace Battler.BattleSystem.Units.Actions.Weapon
{
    public class ProjectileWeapon : Spawner<Projectile>
    {
        public Projectile Spawn(Vector3 position, LayerMask attackTargets, Vector3 shotDirection, bool isUpgraded)
        {
            Projectile projectile = Pool.Get();
            projectile.transform.position = position;
            projectile.Initialize(attackTargets, shotDirection);
            projectile.Collided += OnCollided;
            projectile.Wasted += OnWasted;
            TryAddToActiveList(projectile);

            if (isUpgraded)
                projectile.Upgrade();

            return projectile;
        }

        protected override void Release(Projectile item)
        {
            if (TryRemoveFromActiveList(item) == false)
                return;

            item.Collided -= OnCollided;
            item.Wasted -= OnWasted;
            Pool.Release(item);
        }

        private void OnCollided(Projectile projectile)
        {
            Release(projectile);
        }

        private void OnWasted(Projectile projectile)
        {
            Release(projectile);
        }
    }
}