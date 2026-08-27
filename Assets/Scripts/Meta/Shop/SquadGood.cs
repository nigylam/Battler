using System;

public class SquadGood
{
    public SquadGood(SquadGoodConfig config) 
    {
        if(config == null)
            throw new ArgumentNullException(nameof(config));

        Squad = config.Squad;
        Price = config.Price;
        Available = config.StartAvailable;
        LevelIdOpen = config.LevelIdOpen;
    }

    public SquadPlan Squad { get; private set; }
    public int Price { get; private set; }
    public bool Available { get; private set; }
    public int LevelIdOpen { get; private set; }

    public void Unlock()
    {
        Available = true;
    }
}
