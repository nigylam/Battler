using Battler.UI.SquadView;
using Battler.UI.Tooltip;
using System;
using UnityEngine;

namespace Battler.UI.ShopView
{
    public class ShopPanel : SquadPanel<GoodItem, GoodContext>
    {
        [SerializeField] private ClosedGoodTooltip _goodClosedTooltip;
        [SerializeField] private NotEnoughMoneyTooltip _notEnoughMoneyTooltip;
        [SerializeField] private SquadInfoTooltip _infoTooltip;
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
            DisableTooltip();
        }

        private void OnPointerEnter(GoodItem goodItem, Vector2 position)
        {
            switch (goodItem.State)
            {
                case GoodContext.ItemState.ClosedLevel:
                    _goodClosedTooltip.Enable(goodItem.Good.LevelIdOpen, position);
                    break;
                case GoodContext.ItemState.NotEnoughMoney:
                    _notEnoughMoneyTooltip.Enable(position);
                    break;
                default:
                    _infoTooltip.Enable(goodItem.Good.Squad, position);
                    break;
            }
        }

        private void OnPointerExit()
        {
            DisableTooltip();
        }

        private void DisableTooltip()
        {
            _goodClosedTooltip.Disable();
            _notEnoughMoneyTooltip.Disable();
            _infoTooltip.Disable();
        }
    }
}