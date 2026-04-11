using System.Collections.Generic;
using System;

public class Shop
{
    private List<Good> _goods;

    public Shop(List<Good> goods)
    {
        if(goods == null)
            throw new ArgumentNullException(nameof(goods));

        _goods = goods;
    }

    public IReadOnlyCollection<Good> Goods => _goods;

    public bool TryBuy(Good good, Gold gold, out SquadPlan squad)
    {
        squad = null;

        if(gold.Current < good.Price) 
            return false;

        gold.Spend(good.Price);
        squad = good.Squad;
        return true;
    }
}
