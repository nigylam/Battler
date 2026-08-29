using Battler.UI.SquadView;
using System;
using UnityEngine;

namespace Battler.UI.ShopView
{
    public class ShopPanelContext : PanelContext
    {
        public ShopPanelContext(Func<int, bool> canAfford)
        {
            CanAfford = canAfford;
        }

        public Func<int, bool> CanAfford { get; }
    }
}
