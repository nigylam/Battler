using System;

public class Gold : ICountable
{
    private int _count;

    public Gold() { }

    public event Action Changed;

    public float Max => 9999;

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

    public void Increase(int amount)
    {
        Current += amount;

        if(Current > Max)
            Current = Max;
    }
}
