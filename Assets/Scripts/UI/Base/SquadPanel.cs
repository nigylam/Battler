using System.Collections.Generic;
using UnityEngine;

public abstract class SquadPanel<TItem, TData> : MonoBehaviour where TItem : Item<TData>
{
    [SerializeField] private TItem _itemPrefab;

    private readonly List<TItem> _items = new();

    private bool _subscribed;

    private void OnEnable()
    {
        SubscribeToItems();
    }

    private void OnDisable()
    {
        if (_items.Count > 0)
            foreach (var item in _items)
                UnsubscribeFromItem(item);

        _subscribed = false;
    }

    public void SetItems(IEnumerable<TData> itemsData)
    {
        Clear();

        foreach (TData data in itemsData)
        {
            TItem item = Instantiate(_itemPrefab, transform);
            item.Initialize(data);
            _items.Add(item);
        }

        SubscribeToItems();
    }

    public void Clear()
    {
        if (_items.Count > 0)
        {
            foreach (var item in _items)
            {
                UnsubscribeFromItem(item);
                Destroy(item.gameObject);
            }

            _items.Clear();
        }

        _subscribed = false;
    }

    protected virtual void SubscribeToItem(TItem item) { }
    protected virtual void UnsubscribeFromItem(TItem item) { }

    private void SubscribeToItems()
    {
        if (_subscribed)
            return;

        if (_items.Count > 0)
            foreach (var item in _items)
                SubscribeToItem(item);

        _subscribed = true;
    }
}