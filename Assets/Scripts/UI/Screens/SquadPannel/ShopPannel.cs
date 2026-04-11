using System;
using System.Collections.Generic;
using UnityEngine;

public class ShopPannel : MonoBehaviour
{
    [SerializeField] private GoodItem _itemPrefab;

    private List<GoodItem> _items = new();

    public event Action<Good> Buy;

    private void OnEnable()
    {
        foreach (GoodItem item in _items)
            item.Buy += OnBuyItem;
    }

    private void OnDisable()
    {
        foreach (GoodItem item in _items)
            item.Buy -= OnBuyItem;
    }

    public void SetGoods(IReadOnlyCollection<Good> goods)
    {
        foreach (Good good in goods) 
        {
            GoodItem item = Instantiate(_itemPrefab, transform);
            item.Initialize(good);
            _items.Add(item);
        }
    }

    private void OnBuyItem(Good good)
    {
        Buy?.Invoke(good);
    }
}
