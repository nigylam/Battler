using UnityEngine;

namespace Battler.BattleSystem.Units.Actions.Weapon
{
    public class ProjectileWeapon : Spawner<Projectile>
    {
        [SerializeField] private Projectile.Settings _settings;

        public Projectile Shoot(Vector3 position, LayerMask attackTargets, Vector3 shotDirection, int damageValue)
        {
            Projectile projectile = Pool.Get();
            projectile.gameObject.SetActive(true);
            projectile.transform.position = position;
            projectile.transform.parent = transform;
            projectile.Initialize(_settings, shotDirection, damageValue, attackTargets);
            projectile.Collided += OnCollided;
            projectile.Wasted += OnWasted;
            TryAddToActiveList(projectile);

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