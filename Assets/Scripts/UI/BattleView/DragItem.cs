using Battler.Battle.DragAndDrop;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Battler.UI.BattleView
{
    public class DragItem : ArmyItem, IDragable, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [SerializeField] private TextCounter _textCounter;

        private ItemCounter _itemsCount;
        private bool _counterSubscribed;

        public event Action<DragItem> DragStarted;
        public event Action<PointerEventData> Dragged;
        public event Action<PointerEventData> DragEnded;

        private void OnEnable()
        {
            SubscribeCounter();
        }

        private void OnDisable()
        {
            UnsubscribeCounter();
        }

        public override void Initialize(SquadData data)
        {
            base.Initialize(data);
            _itemsCount = new ItemCounter(data.Count);
            _textCounter.Initialize(_itemsCount);
            SubscribeCounter();
        }

        public void OnBeginDrag(PointerEventData eventData) => DragStarted?.Invoke(this);
        public void OnDrag(PointerEventData eventData) => Dragged?.Invoke(eventData);
        public void OnEndDrag(PointerEventData eventData) => DragEnded?.Invoke(eventData);

        public void Decrease() => _itemsCount.Decrease();

        public void Increase() => _itemsCount.Increase();

        private void OnEnded() => gameObject.SetActive(false);

        private void SubscribeCounter()
        {
            if (_itemsCount == null)
                return;

            if (_counterSubscribed)
                return;

            _textCounter.Enable();
            _itemsCount.Ended += OnEnded;
            _counterSubscribed = true;
        }

        private void UnsubscribeCounter()
        {
            _textCounter.Disable();
            _itemsCount.Ended -= OnEnded;
            _counterSubscribed = false;
        }
    }
}