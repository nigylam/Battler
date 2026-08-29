using System;
using UnityEngine;

public class Health : ICountable
{
    private readonly int _max;
    private readonly int _maxUpgraded;

    private float _current;
    private bool _active = true;

    public event Action Dead;
    public event Action Changed;

    public Health(int max, int maxUpgraded)
    {
        _max = max;
        _maxUpgraded = maxUpgraded;
        _current = max;
        Max = _max;
    }

    public float Max { get; private set; }

    public float Current
    {
        get { return _current; }
        private set
        {
            _current = value;
            Changed?.Invoke();
        }
    }

    public void TakeDamage(float damage)
    {
        if (_active == false)
            return;

        Current -= damage;

        if (Current <= 0)
        {
            _active = false;
            Dead?.Invoke();
        }
    }

    public void Heal(int count)
    {
        if (_active == false)
            return;

        Current += count;

        if (Current > _max)
            Current = _max;
    }

    public void Restart()
    {
        Current = _max;
        _active = true;
    }

    public void Upgrade()
    {
        Max = _maxUpgraded;
        Current = Max;
    }
}