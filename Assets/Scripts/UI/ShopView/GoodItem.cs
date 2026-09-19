using Battler.UI.SquadView;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Battler.UI.ShopView
{
    public class GoodItem : Item<GoodContext>, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private TextMeshProUGUI _price;
        [SerializeField] private UIButton _buyButton;
        [SerializeField] private GameObject _notAvailableMask;

        public event Action<SquadGood> Buy;
        public event Action<GoodItem, Vector2> PointerEnter;
        public event Action PointerExit;

        public SquadGood Good { get; private set; }
        public GoodContext.ItemState State { get; private set; }

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
            PointerEnter?.Invoke(this, transform.position);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            PointerExit?.Invoke();
        }

        public override void Initialize(GoodContext good)
        {
            State = good.State;
            Good = good.SquadGood;
            SetSquad(good.SquadGood.Squad);
            _price.text = good.SquadGood.Price.ToString();
            _buyButton.gameObject.SetActive(State == GoodContext.ItemState.CanBuy);
            _notAvailableMask.SetActive(State == GoodContext.ItemState.ClosedLevel);
        }

        private void OnBuyClick()
        {
            Buy?.Invoke(Good);
        }
    }
}