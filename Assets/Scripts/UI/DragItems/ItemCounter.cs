using System;
using UnityEngine;

public class ItemCounter : MonoBehaviour, ICountable
{
    [SerializeField] private int _count;

    private int _maxCount = 99;

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
