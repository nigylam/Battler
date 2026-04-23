using Battler.Core.SquadKeeping;
using Battler.Meta;
using System;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Battler.UI.ShopView
{
    public class ShopMenu : MonoBehaviour
    {
        [SerializeField] private ArmyPanel _armyPanel;
        [SerializeField] private ShopPanel _shopPanel;
        [SerializeField] private TextCounter _goldCounter;
        [SerializeField] private Button _exitButton;

        private Shop _shop;
        private Gold _gold;
        private GameSquadKeeper _keeper;

        public event Action Exit;

        private void OnEnable()
        {
            _exitButton.onClick.AddListener(OnExitClick);
            _shopPanel.Buy += OnBuyGood;
            _goldCounter.Enable();
        }

        private void OnDisable()
        {
            _exitButton.onClick.RemoveListener(OnExitClick);
            _shopPanel.Buy -= OnBuyGood;
            _goldCounter.Disable();
        }

        public void Initialize(Gold gold, Shop shop, GameSquadKeeper keeper)
        {
            _goldCounter.Initialize(gold);
            _shop = shop;
            _gold = gold;
            _keeper = keeper;
            _armyPanel.SetItems(_keeper);
            _shopPanel.SetItems(_shop);
        }

        private void OnExitClick()
        {
            _exitButton.onClick.RemoveListener(OnExitClick);
            Exit?.Invoke();
        }

        private void OnBuyGood(SquadGood good)
        {
            if (_shop.TryBuy(good, _gold, out SquadPlan squad))
            {
                GameSquadCell squadCell = new(squad, 1);
                _keeper.AddSquad(squadCell);
                YG2.saves.boughtSquads.Add(squadCell.Plan);
                YG2.SaveProgress();
            }
        }
    }
}