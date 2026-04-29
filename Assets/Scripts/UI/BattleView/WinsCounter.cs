using System;
using UnityEngine;

public class WinsCounter : MonoBehaviour, ICountable
{
    private int _max;
    private int _count = 0;

    public event Action Changed;
    public event Action Win;

    public float Max => _max;

    public float Current
    {
        get
        {
            return _count;
        }
        private set
        {
            _count = (int)value;
            Changed?.Invoke();
        }
    }

    public void Initialize(int max)
    {
        _max = max;
    }

    public void Increase()
    {
        Current++;

        if (Current == _max)
            Win?.Invoke();
    }

    public void Restart()
    {
        Current = 0;
    }
}
