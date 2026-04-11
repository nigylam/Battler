using System;
using System.Collections.Generic;
using UnityEngine;

public class DragArmyPannel : MonoBehaviour
{
    [SerializeField] private DragItem _itemPrefab;

    private List<DragItem> _items = new();

    public event Action<DragItem> DragStarted;

    private void OnEnable()
    {
        if (_items.Count == 0)
            return;

        foreach (var item in _items)
            item.DragStarted += OnDragStarted;
    }

    private void OnDisable()
    {
        if (_items.Count == 0)
            return;

        foreach (var item in _items)
            item.DragStarted -= OnDragStarted;
    }

    public void SetItems(SquadKeeper keeper)
    {
        foreach (SquadPlan squad in keeper.Squads)
        {
            DragItem item = Instantiate(_itemPrefab, transform);
            item.Initialize(squad, keeper.GetSquadsCount(squad));
            Instantiate(squad.Preview, transform);
            _items.Add(item);
        }
    }

    public void Clear()
    {
        if(_items.Count == 0)
            return;

        while(_items.Count > 0)
        {
            DragItem item = _items[0];
            _items.Remove(item);
            item.DragStarted -= OnDragStarted;
            Destroy(item.gameObject);
        }
    }

    private void OnDragStarted(DragItem item)
    {
        DragStarted?.Invoke(item);
    }
}
