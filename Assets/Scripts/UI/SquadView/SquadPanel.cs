using System;
using System.Collections.Generic;
using UnityEngine;

namespace Battler.UI.SquadView
{
    public abstract class SquadPanel<TItem, TSquad> : MonoBehaviour where TItem : Item<TSquad>
    {
        [SerializeField] private TItem _itemPrefab;
        [SerializeField] private Transform _itemParrent;

        private readonly List<TItem> _items = new();

        private ISquadViewable<TSquad> _viewable;
        private bool _subscribed;

        public IReadOnlyCollection<TItem> Items => _items;

        protected virtual PanelContext PanelContext { get; }

        private void OnEnable()
        {
            Subscribe();
        }

        private void OnDisable()
        {
            Unsubscribe();
        }

        public void SetItems(ISquadViewable<TSquad> viewable)
        {
            _viewable = viewable;
            UpdateItems();
        }

        protected virtual void SubscribeToItem(TItem item) { }
        protected virtual void UnsubscribeFromItem(TItem item) { }

        private void UpdateItems()
        {
            Clear();

            foreach (TSquad squad in _viewable.Squads)
            {
                AddItem(squad);
            }

            Subscribe();
        }

        private void Clear()
        {
            Unsubscribe();

            if (_items.Count == 0)
                return;

            for (int i = _items.Count - 1; i >= 0; i--)
                Destroy(_items[i].gameObject);

            _items.Clear();
        }

        private void AddItem(TSquad data)
        {
            TItem item = Instantiate(_itemPrefab, _itemParrent);
            item.Initialize(data, PanelContext);
            _items.Add(item);
        }

        private void Subscribe()
        {
            if (_subscribed)
                return;

            _viewable.Changed += UpdateItems;

            if (_items.Count > 0)
                foreach (var item in _items)
                    SubscribeToItem(item);

            _subscribed = true;
        }

        private void Unsubscribe()
        {
            if (_items.Count > 0)
                foreach (var item in _items)
                    UnsubscribeFromItem(item);

            _subscribed = false;
        }
    }
}