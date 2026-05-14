using Battler.BattleSystem.Units.Actions;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Battler.BattleSystem.Units
{
    public class UnitNew : MonoBehaviour
    {
        [SerializeField] private Health _health;
        [SerializeField] private TargetFinder _targetFinder;
        [SerializeField] private UnitVisual _visual;
        [SerializeField] private UnitAction _action;
        [SerializeField] private Mover _mover;

        private Unit _target;
        private bool _isDead;

        public event Action<UnitNew> Dead;
        public event Action<UnitNew> Free;

        public UnitVisual Visual => _visual;

        private void OnEnable()
        {
            _health.Dead += OnDead;

            if (_mover != null)
            {
                _mover.WentToTarget += OnWentTarget;
                _mover.LeaveTarget += OnLeaveTarget;
            }
        }

        private void OnDisable()
        {
            _health.Dead -= OnDead;

            if (_target != null)
                _target.Dead -= OnTargetDead;

            if (_mover != null)
            {
                _mover.WentToTarget -= OnWentTarget;
                _mover.LeaveTarget -= OnLeaveTarget;
            }
        }

        public void Initialize(Material armyMaterial, LayerMask targetLayer)
        {
            _visual.SetMaterial(armyMaterial);
            _action.Initialize(targetLayer);
        }

        public void Upgrade(bool playAnimation)
        {
            _health.Upgrade();
            _action.Upgrade();
            _visual.Upgrade();

            if (_mover != null)
                _mover.Upgrade();

            if (playAnimation)
            {
                _visual.PlayUpgrade();
            }
        }

        public void SetTarget(List<Unit> potentialTargets)
        {
            if (_isDead || potentialTargets.Count == 0)
                return;

            _target = _targetFinder.GetTarget(potentialTargets);

            if (_target == null)
                return;

            _target.Dead += OnTargetDead;

            if (_mover != null)
            {
                _mover.SetTarget(_target.transform);
            }
            else
            {
                OnWentTarget();
            }
        }

        public void TakeDamage(int damage)
        {
            _health.TakeDamage(damage);
            _visual.PlayHitAnimation();
        }

        public void Heal(int count)
        {
            _health.Heal(count);
        }

        private void OnWentTarget()
        {
            if (_action != null)
                _action.StartAction(_target);
        }

        private void OnLeaveTarget()
        {
            if (_action != null)
                _action.StopAction();
        }

        public void Stop()
        {

            _action.StopAction();
        }

        private void OnDead()
        {
            Stop();
            _isDead = true;
            _visual.PlayDeathAnimation();
            Dead?.Invoke(this);
        }

        private void OnTargetDead(Unit _)
        {
            if (_isDead)
                return;

            _target.Dead -= OnTargetDead;
            _target = null;

            Free?.Invoke(this);
        }
    }
}
