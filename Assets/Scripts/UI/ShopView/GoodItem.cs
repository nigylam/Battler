using Battler.UI.SquadView;
using System;
using TMPro;
using UnityEngine;

namespace Battler.UI.ShopView
{
    public class GoodItem : Item<SquadGood>
    {
        [SerializeField] private TextMeshProUGUI _price;
        [SerializeField] private UIButton _buyButton;
        [SerializeField] private GameObject _notAvailableMask;

        private SquadGood _good;

        public event Action<SquadGood> Buy;

        private void OnEnable()
        {
            _buyButton.Clicked += OnBuyClick;
        }

        private void OnDisable()
        {
            _buyButton.Clicked -= OnBuyClick;
        }

        public override void Initialize(SquadGood good)
        {
            _good = good;
            SetSquad(good.Squad);
            _price.text = good.Price.ToString();
            _notAvailableMask.SetActive(good.Available == false);
            _buyButton.SetInteractable(good.Available);
        }

        private void OnBuyClick()
        {
            Buy?.Invoke(_good);
        }
    }
}