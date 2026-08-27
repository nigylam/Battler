using Battler;
using Battler.UI.ShopView;
using System;
using UnityEngine;

public class ShopPanel : SquadPanel<GoodItem, SquadGood>
{
    [SerializeField] private Tooltip _goodClosedTooltip;
    [SerializeField] private Vector2 _tooltipPositionOffset;

    public event Action<SquadGood> Buy;

    protected override void SubscribeToItem(GoodItem item)
    {
        item.Buy += OnBuyItem;
        item.PointerEnter += OnPointerEnter;
        item.PointerExit += OnPointerExit;
    }

    protected override void UnsubscribeFromItem(GoodItem item)
    {
        item.Buy -= OnBuyItem;
        item.PointerEnter -= OnPointerEnter;
        item.PointerExit -= OnPointerExit;
    }

    private void OnBuyItem(SquadGood good)
    {
        Buy?.Invoke(good);
    }

    private void OnPointerEnter(SquadGood good, Vector2 position)
    {
        if (good.Available == false)
        {
            _goodClosedTooltip.Enable(good.LevelIdOpen, position + _tooltipPositionOffset);
        }
    }

    private void OnPointerExit()
    {
        _goodClosedTooltip.Disable();
    }
}
