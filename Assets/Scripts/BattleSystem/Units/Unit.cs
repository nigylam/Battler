using Battler.BattleSystem.Units.Actions;
using Battler.BattleSystem.Units.TargetFinding;
using Battler.BattleSystem.Units.Visual;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Battler.BattleSystem.Units
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] private UnitStats _stats;
        [SerializeField] private TargetFinder _targetFinder;
        [SerializeField] private UnitVisual _visual;
        [SerializeField] private UnitAction _action;
        [SerializeField] private Mover _mover;
        [SerializeField] private Collider _collider;
        [SerializeField] private Sound _sound;
        [SerializeField] private HealthBar _healthBar;
        [SerializeField] private bool _isFlying = false;

        [Header("for testing")]
        [SerializeField] private int _damage;

        private readonly float _targetRequestDelay = 1f;

        private Unit _target;
        private Health _health;
        private bool _subscribed = false;

        public event Action<Unit> Dead;
        public event Action<Unit> NeedTarget;

        public UnitVisual Visual => _visual;
        public bool IsDead { get; private set; }
        public bool HealthDecreased => _health.Current < _health.Max;
        public Transform Body => _visual.Body;
        public bool IsFlying => _isFlying;
        public UnitStats Stats => _stats;

        [ContextMenu("health")]
        public void ShowHealth()
        {
            Debug.Log(_health.Current);
        }

        [ContextMenu("damage")]
        public void TakeDamage()
        {
            TakeDamage(_damage, Vector3.zero);
        }

        [ContextMenu("heal")]
        public void Heal()
        {
            Heal(10);
        }

        [ContextMenu("target")]
        public void DebugTarget()
        {
            Debug.Log(_target.ToString());
        }

        private void OnEnable()
        {
            if(_health == null)
                return;

            _health.Restart();
            Subscribe();
        }

        private void Update()
        {
            if (_mover != null)
                _visual.PlayMoveAnimation(_mover.Speed > 0);
        }

        private void OnDisable()
        {
            Unsubscribe();
        }

        public void Initialize(Material armyMaterial, LayerMask targetLayer)
        {
            _visual.SetArmyMaterial(armyMaterial);
            _action.Initialize(targetLayer, _stats.ActionValue, _stats.ActionValueUpgraded);
            _health = new Health(_stats.Health, _stats.HealthUpgraded);
            _healthBar.Initialize(_health);
            Subscribe();
        }

        public void Upgrade(bool playAnimation)
        {
            _health.Upgrade();
            _action.Upgrade();
            _visual.Upgrade();

            if (_mover != null)
                _mover.Upgrade();

            if (playAnimation)
                _visual.PlayUpgrade();
        }

        public void TakeDamage(int damage, Vector3 hitPoint)
        {
            _health.TakeDamage(damage);
            _visual.OnHit(hitPoint);
        }

        public void Heal(int count)
        {
            _health.Heal(count);
            _visual.OnHeal();
            _healthBar.OnHeal();
        }

        public async UniTask StartCombat()
        {
            while (IsDead == false)
            {
                if (await SetTargetAsync().SuppressCancellationThrow())
                    return;

                if (await MoveToTarget().SuppressCancellationThrow())
                    return;

                if (await PerformAction().SuppressCancellationThrow())
                    return;
            }
        }

        public void SetTarget(List<Unit> enemies, List<Unit> alies)
        {
            _target = _targetFinder.GetTarget(enemies, alies);
        }

        public void Stop()
        {
            _action.Stop();
        }

        public Vector3 GetClosestPoint(Vector3 point)
        {
            return _collider.ClosestPoint(point);
        }

        private void Subscribe()
        {
            if (_subscribed)
                return;

            _health.Dead += OnDead;
            _subscribed = true;
        }

        private void Unsubscribe()
        {
            _health.Dead -= OnDead;
            _subscribed = false;
        }

        private void OnDead()
        {
            if (_sound != null)
                _sound.PlayDeathSound();

            _action.Stop();
            IsDead = true;
            _visual.PlayDeathAnimation();
            Dead?.Invoke(this);
        }

        private async UniTask PerformAction()
        {
            if (_target == null || _target.IsDead)
                return;

            Func<bool> isClose = () => _mover == null || _mover.CloseToTarget();
            await _action.StartAction(_target, isClose);

            if (_action.IsSingleAction)
                _target = null;
        }

        private async UniTask MoveToTarget()
        {
            if (_mover == null)
                return;

            _mover.SetTarget(_target);
            await UniTask.WaitUntil(() => _mover.CloseToTarget() || _target.IsDead, cancellationToken: this.GetCancellationTokenOnDestroy());
        }

        private async UniTask SetTargetAsync()
        {
            while (_target == null || _target.IsDead)
            {
                NeedTarget?.Invoke(this);
                await UniTask.Delay(TimeSpan.FromSeconds(_targetRequestDelay), cancellationToken: this.GetCancellationTokenOnDestroy());
            }
        }
    }
}