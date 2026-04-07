using System;

public class RoundCounter : ICountable
{
    private int _count = 0;
    private int _maxCount = 99;

    public event Action Changed;

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

    public void Increase()
    {
        Current++;
    }

    public void Restart()
    {
        Current = 0;
    }
}
