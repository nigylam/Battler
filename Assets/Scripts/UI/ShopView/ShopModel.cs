using Battler.Meta;
using Battler.UI.SquadView;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battler.UI.ShopView
{
    public class ShopModel : ISquadViewable<GoodItemContext>
    {
        private readonly Shop _shop;
        private readonly Gold _gold;

        public ShopModel(Shop shop, Gold gold)
        {
            _shop = shop;
            _gold = gold;
            Squads = GetSquads();
        }

        public IReadOnlyList<GoodItemContext> Squads { get; private set; }

        public event Action Changed;

        public void Enable()
        {
            _shop.Changed += OnChanged;
            _gold.Changed += OnChanged;
        }

        public void Disable() 
        {
            _shop.Changed -= OnChanged;
            _gold.Changed -= OnChanged;
        }

        private List<GoodItemContext> GetSquads()
        {
            List<GoodItemContext> goods = new();

            foreach(var good in _shop.Squads)
            {
                goods.Add(new GoodItemContext(good, CanAfford(good)));
            }

            return goods;
        }

        private bool CanAfford(SquadGood good)
        {
            return good.Price <= _gold.Current;
        }

        private void OnChanged()
        {
            Squads = GetSquads();
            Changed?.Invoke();
        }
    }
}
