using System;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _attackRange;
    [SerializeField] private float _attackRangeUpgraded;
    [SerializeField] private float _shootRotationOffset;
    [SerializeField] private NavMeshAgent _agent;

    private Transform _target;

    private float _updateRate = 0.2f;
    private float _timer;
    private float _rotationSpeed = 120f;
    private float _angleAttackOffset = 0.001f;
    private bool _wentEventSended = false;
    private bool _leaveEventSended = false;
    private bool _isEnabled = false;

    public event Action WentToTarget;
    public event Action LeaveTarget;

    public float Speed => _agent.velocity.magnitude;

    private void Awake()
    {
        _agent.updateRotation = false;
    }

    private void Update()
    {
        if (_isEnabled == false)
            return;

        RotateTowardsTarget();

        if (_agent.enabled == false)
            return;

        MoveNavmesh();
    }

    public void Enable()
    {
        _isEnabled = true;
    }

    public void Disable()
    {
        _isEnabled = false;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
        _wentEventSended = false;
        _agent.isStopped = false;
        _agent.SetDestination(_target.position);
    }

    public void Upgrade()
    {
        _attackRange = _attackRangeUpgraded;
    }

    private bool CloseToTarget()
    => Vector3.SqrMagnitude(transform.position - _target.position) <= _attackRange * _attackRange;

    private void MoveNavmesh()
    {
        _timer -= Time.deltaTime;

        if (_timer > 0)
            return;

        _timer = _updateRate;

        if (CloseToTarget() == false)
        {
            _agent.isStopped = false;
            _agent.SetDestination(_target.position);

            if (_leaveEventSended == false)
            {
                LeaveTarget?.Invoke();
                _leaveEventSended = true;
            }
        }
        else
        {
            _agent.isStopped = true;
            _agent.velocity = Vector3.zero;

            if (_wentEventSended == false)
            {
                WentToTarget?.Invoke();
                _wentEventSended = true;
            }
        }
    }

    private void RotateTowardsTarget()
    {
        Vector3 direction = _target.position - transform.position;
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
