using System;
using UnityEngine;

public class ShopPannel : SquadPanel<GoodItem, Good>
{
    public event Action<Good> Buy;

    protected override void SubscribeToItem(GoodItem item)
    {
        item.Buy += OnBuyItem;
    }

    protected override void UnsubscribeFromItem(GoodItem item)
    {
        item.Buy -= OnBuyItem;
    }

    private void OnBuyItem(Good good)
    {
        Buy?.Invoke(good);
    }
}
