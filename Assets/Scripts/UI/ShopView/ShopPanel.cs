using System;
using UnityEngine;

public class ShopPanel : SquadPanel<GoodItem, SquadGood>
{
    public event Action<SquadGood> Buy;

    protected override void SubscribeToItem(GoodItem item)
    {
        item.Buy += OnBuyItem;
    }

    protected override void UnsubscribeFromItem(GoodItem item)
    {
        item.Buy -= OnBuyItem;
    }

    private void OnBuyItem(SquadGood good)
    {
        Buy?.Invoke(good);
    }
}
