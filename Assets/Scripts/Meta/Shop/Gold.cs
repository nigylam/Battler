using System;
using YG;

public class Gold : ICountable
{
    private int _count;

    public Gold() 
    {
        _count = YG2.saves.gold;
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
            YG2.saves.gold = (int)Current;
            YG2.SaveProgress();
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
