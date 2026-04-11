using System;

public class Gold : ICountable
{
    private int _count;

    public Gold() 
    {
        _count = 100;
    }

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

    public void Spend(int price)
    {
        if(price < 0 || Current - price < 0)
            throw new ArgumentOutOfRangeException(nameof(price));

        Current -= price;
    }
}
