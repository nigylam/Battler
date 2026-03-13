using System;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _attackRange;

    private Transform _target;
    private NavMeshAgent _agent;

    private float _updateRate = 0.2f;
    private float _timer;
    private float _rotationSpeed = 120f;
    private float _angleAttackOffset = 0.001f;
    private bool _wentEventSended = false;
    private bool _leaveEventSended = false;

    public event Action WentToTarget;
    public event Action LeaveTarget;

    public float Speed => _agent.velocity.magnitude;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (_agent.enabled == false)
            return;

        RotateTowardsTarget();
        MoveNavmesh();
    }

    public void Initialize(NavMeshAgent agent)
    {
        _agent = agent;
        _agent.updateRotation = false;
        _agent.enabled = false;
        _agent.radius *= transform.lossyScale.x;
        _agent.height *= transform.lossyScale.y;
    }

    public void Disable()
    {
        _agent.enabled = false;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
        _agent.SetDestination(_target.position);
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
            _agent.enabled = true;
            _agent.SetDestination(_target.position);

            if (_leaveEventSended == false)
            {
                LeaveTarget?.Invoke();
                _leaveEventSended = true;
            }
        }
        else
        {
            _agent.enabled = false;

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
