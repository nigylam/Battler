using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Battler.UI.BattleView
{
    public class DragItem : ArmyItem, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [SerializeField] private TextCounter _textCounter;

        private ItemCounter _itemsCount;

        public event Action<DragItem> DragStarted;
        public event Action<PointerEventData> Dragged;
        public event Action<PointerEventData> DragEnded;

        private void OnEnable() => _itemsCount.Ended += OnItemsEnded;

        private void OnDisable()
        {
            _textCounter.Disable();
            _itemsCount.Ended -= OnItemsEnded;
        }

        public override void Initialize(SquadData data)
        {
            base.Initialize(data);
            _itemsCount = new ItemCounter(data.Count);
            _textCounter.Initialize(_itemsCount);
        }

        public void OnBeginDrag(PointerEventData eventData) => DragStarted?.Invoke(this);
        public void OnDrag(PointerEventData eventData) => Dragged?.Invoke(eventData);
        public void OnEndDrag(PointerEventData eventData) => DragEnded?.Invoke(eventData);

        public void Decrease() => _itemsCount.Decrease();

        private void OnItemsEnded() => gameObject.SetActive(false);
    }
}