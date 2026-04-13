using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GoodItem : Item<Good>
{
    [SerializeField] private TextMeshProUGUI _price;
    [SerializeField] private Button _buyButton;
    [SerializeField] private GameObject _notAvailableMask;

    private Good _good;

    public event Action<Good> Buy;

    private void OnEnable()
    {
        _buyButton.onClick.AddListener(OnBuyClick);
    }

    private void OnDisable()
    {
        _buyButton.onClick.RemoveListener(OnBuyClick);
    }

    public override void Initialize(Good good)
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
