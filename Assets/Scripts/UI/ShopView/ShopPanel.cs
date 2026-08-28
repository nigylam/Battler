using Battler.UI.ShopView;
using Battler.UI.SquadView;
using Battler.UI.Tooltip;
using System;
using UnityEngine;

public class ShopPanel : SquadPanel<GoodItem, SquadGood>
{
    [SerializeField] private ClosedGoodTooltip _goodClosedTooltip;
    [SerializeField] private NotEnoughMoneyTooltip _notEnoughMoneyTooltip;
    [SerializeField] private SquadInfoTooltip _infoTooltip;
    [SerializeField] private Vector2 _tooltipPositionOffset;

    private Func<int, bool> _canAffordCheck;

    public event Action<SquadGood> Buy;

    public void Initialize(ISquadViewable<SquadGood> shop, Func<int, bool> canAffordCheck)
    {
        _canAffordCheck = canAffordCheck ?? throw new ArgumentNullException(nameof(canAffordCheck));
        SetItems(shop);
    }

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
            _goodClosedTooltip.Enable(good.LevelIdOpen, position);
        else if(_canAffordCheck(good.Price) == false)
            _notEnoughMoneyTooltip.Enable(position);
        else
            _infoTooltip.Enable(good.Squad, position);
    }

    private void OnPointerExit()
    {
        _goodClosedTooltip.Disable();
        _notEnoughMoneyTooltip.Disable();
        _infoTooltip.Disable();
    }
}
