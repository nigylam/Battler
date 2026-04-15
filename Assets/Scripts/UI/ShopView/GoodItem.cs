using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GoodItem : Item<SquadGood>
{
    [SerializeField] private TextMeshProUGUI _price;
    [SerializeField] private Button _buyButton;
    [SerializeField] private GameObject _notAvailableMask;

    private SquadGood _good;

    public event Action<SquadGood> Buy;

    private void OnEnable()
    {
        _buyButton.onClick.AddListener(OnBuyClick);
    }

    private void OnDisable()
    {
        _buyButton.onClick.RemoveListener(OnBuyClick);
    }

    public override void Initialize(SquadGood good)
    {
        _good = good;
        SetSquad(good.Squad);
        _price.text = good.Price.ToString();
        _notAvailableMask.gameObject.SetActive(good.Available == false);
    }

    private void OnBuyClick()
    {
        Buy?.Invoke(_good);
    }
}
