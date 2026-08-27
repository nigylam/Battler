using Battler.UI.SquadView;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Battler.UI.ShopView
{
    public class GoodItem : Item<SquadGood>, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private TextMeshProUGUI _price;
        [SerializeField] private UIButton _buyButton;
        [SerializeField] private GameObject _notAvailableMask;

        private SquadGood _good;

        public event Action<SquadGood> Buy;
        public event Action<SquadGood, Vector2> PointerEnter;
        public event Action PointerExit;

        private void OnEnable()
        {
            _buyButton.Clicked += OnBuyClick;
        }

        private void OnDisable()
        {
            _buyButton.Clicked -= OnBuyClick;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            PointerEnter?.Invoke(_good, transform.position);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            PointerExit?.Invoke();
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