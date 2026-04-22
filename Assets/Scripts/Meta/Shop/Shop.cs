using System.Collections.Generic;
using System;
using System.Linq;
using Battler;
using Battler.UI.SquadView;

public class Shop : ISquadViewable<SquadGood>
{
    private readonly Dictionary<SquadGoodConfig, SquadGood> _goods;

    public event Action Changed;

    public IReadOnlyList<SquadGood> Squads => _goods.Values.ToList();

    public Shop(List<SquadGoodConfig> goods) : base()
    {
        if(goods == null)
            throw new ArgumentNullException(nameof(goods));

        _goods = new Dictionary<SquadGoodConfig, SquadGood>();

        foreach (SquadGoodConfig good in goods)
        {
            if(good == null)
                throw new ArgumentNullException(nameof(good));

            _goods.Add(good, new SquadGood(good));
        }
    }

    public bool TryBuy(SquadGood good, Gold gold, out SquadPlan squad)
    {
        if(good == null)
            throw new ArgumentNullException(nameof(good));

        if(gold == null)
            throw new ArgumentNullException(nameof(gold));

        if (_goods.Values.Contains(good) == false)
            throw new InvalidOperationException(nameof(TryBuy));

        squad = null;

        if(gold.Current < good.Price) 
            return false;

        gold.Spend(good.Price);
        squad = good.Squad;
        return true;
    }

    public void Unlock(SquadGoodConfig goodConfig)
    {
        if(goodConfig == null)
            throw new ArgumentNullException(nameof(goodConfig));

        if(_goods.Keys.Contains(goodConfig) == false)
            throw new InvalidOperationException(nameof(Unlock));

        _goods[goodConfig].Unlock();
        Changed?.Invoke();
    }
}
