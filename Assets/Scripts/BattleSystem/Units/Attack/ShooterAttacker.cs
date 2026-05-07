using Battler.BattleSystem.Units.Animation.SoldierRunning;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battler.BattleSystem.Units.Attack
{
    public class ShooterAttacker : ProjectileAttacker
    {
        [SerializeField] private ShootAnimationEventSender _shootAnimationEventSender;

        private bool _canShoot;

        protected override bool CanAttack => _canShoot;

        public override void StartAttack()
        {
            base.StartAttack();
            _shootAnimationEventSender.ReadyShoot += OnReadyShoot;
            _canShoot = false;
        }

        protected override void Disable()
        {
            _shootAnimationEventSender.ReadyShoot -= OnReadyShoot;
        }

        private void OnReadyShoot()
        {
            _shootAnimationEventSender.ReadyShoot -= OnReadyShoot;
            _canShoot = true;
        }
    }
}
