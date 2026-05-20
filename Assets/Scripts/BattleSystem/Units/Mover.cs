using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Battler.BattleSystem.Units
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] private float _attackRange;
        [SerializeField] private float _attackRangeUpgraded;
        [SerializeField] private NavMeshAgent _agent;

        private readonly float _updateRate = 0.2f;
        private readonly float _rotationSpeed = 120f;
        private readonly float _angleAttackOffset = 0.001f;

        private Unit _target;
        private Coroutine _currentBehavior;

        public float Speed => _agent.velocity.magnitude;

        private void Awake()
        {
            _agent.updateRotation = false;
        }

        private void Update()
        {
            RotateTowardsTarget();
        }

        private void OnDisable()
        {
            if (_currentBehavior != null)
                StopCoroutine(_currentBehavior);
        }

        public void SetTarget(Unit target)
        {
            _target = target;
            _agent.isStopped = false;
            SwitchState(MoveRoutine());
        }

        public void Upgrade()
        {
            _attackRange = _attackRangeUpgraded;
        }

        public bool CloseToTarget()
        => Vector3.SqrMagnitude(transform.position - _target.GetClosestPoint(transform.position)) <= _attackRange * _attackRange;

        private void SwitchState(IEnumerator newState)
        {
            StopCurrentBehavior();
            _currentBehavior = StartCoroutine(newState);
        }

        private void StopCurrentBehavior()
        {
            if (_currentBehavior != null)
            {
                StopCoroutine(_currentBehavior);
                _currentBehavior = null;
            }
        }

        private IEnumerator MoveRoutine()
        {
            if (_agent.enabled && _agent.isOnNavMesh)
                _agent.isStopped = false;

            float pathTimer = 0f;

            while (_target != null && CloseToTarget() == false)
            {
                pathTimer -= Time.deltaTime;

                if (pathTimer <= 0f)
                {
                    pathTimer = _updateRate;

                    if (_agent.enabled && _agent.isOnNavMesh)
                        _agent.SetDestination(_target.transform.position);
                }

                yield return null;
            }

            if (_agent.enabled && _agent.isOnNavMesh)
            {
                _agent.isStopped = true;
                _agent.velocity = Vector3.zero;
            }

            SwitchState(MonitorRoutine());
        }

        private IEnumerator MonitorRoutine()
        {
            while (_target != null && CloseToTarget())
                yield return new WaitForSeconds(0.1f);

            if (_target != null)
                SwitchState(MoveRoutine());
        }

        private void RotateTowardsTarget()
        {
            if (_target == null)
                return;

            Vector3 direction = _target.transform.position - transform.position;
            direction.y = 0f;

            if (direction.sqrMagnitude < _angleAttackOffset)
                return;

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                _rotationSpeed * Time.deltaTime
            );
        }
    }
}