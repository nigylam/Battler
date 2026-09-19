using Battler.Core.SquadKeeping;
using Battler.UI.SquadView;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

namespace Battler.Meta
{
    public class ShopModel : ISquadViewable<GoodContext>
    {
        private readonly Shop _shop;

        public ShopModel(Shop shop, Gold gold, GameSquadKeeper squadKeeper)
        {
            _shop = shop;
            Gold = gold;
            SquadKeeper = squadKeeper;
            Squads = GetSquads();
        }

        public IReadOnlyList<GoodContext> Squads { get; private set; }
        public GameSquadKeeper SquadKeeper { get; }
        public Gold Gold { get; }

        public event Action Changed;

        public void Enable()
        {
            _shop.Changed += OnChanged;
            Gold.Changed += OnChanged;
            OnChanged();
        }

        public void Disable()
        {
            _shop.Changed -= OnChanged;
            Gold.Changed -= OnChanged;
        }

        public bool TryBuyGood(SquadGood good)
        {
            if (_shop.TryBuy(good, Gold, out SquadPlan squad) == false)
                return false;

            GameSquadCell squadCell = new(squad, 1);
            SquadKeeper.AddSquad(squadCell);
            YG2.saves.AddBoughtSquad(squadCell.Plan.Id);
            YG2.SaveProgress();
            return true;
        }

        private List<GoodContext> GetSquads()
        {
            List<GoodContext> goods = new();

            foreach (var good in _shop.Squads)
            {
                var state = GoodContext.ItemState.CanBuy;

                if (CanAfford(good) == false)
                    state = GoodContext.ItemState.NotEnoughMoney;
                if (good.Available == false)
                    state = GoodContext.ItemState.ClosedLevel;

                goods.Add(new GoodContext(good, state));
            }

            return goods;
        }

        private bool CanAfford(SquadGood good)
        {
            return good.Price <= Gold.Current;
        }

        private void OnChanged()
        {
            Squads = GetSquads();
            Changed?.Invoke();
        }
    }
}
