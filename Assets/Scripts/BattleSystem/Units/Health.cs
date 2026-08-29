using System;
using UnityEngine;

public class Health : ICountable
{
    private float _current;
    private int _max;
    private bool _active = true;

    public event Action Dead;
    public event Action Changed;

    public Health(int max, int maxUpgraded)
    {
        Max = max;
        MaxUpgraded = maxUpgraded;
        _max = max;
        _current = max;
    }

    public float Max { get; }
    public float MaxUpgraded { get; }

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
        _max = (int)MaxUpgraded;
        Current = _max;
    }
}