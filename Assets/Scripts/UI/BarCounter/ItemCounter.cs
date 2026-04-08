using System;
using UnityEngine;

public class ItemCounter : ICountable
{
    private int _count;
    private int _maxCount = 99;

    public ItemCounter(int count)
    {
        Current = count;
    }

    public event Action Changed;
    public event Action Ended;

    public float Max => _maxCount;

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

    public void Decrease()
    {
        Current--;

        if(Current == 0)
            Ended?.Invoke();
    }
}
