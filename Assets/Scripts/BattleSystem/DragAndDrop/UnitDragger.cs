using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Battler.BattleSystem.DragAndDrop
{
    public class UnitDragger : MonoBehaviour, IDragable, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        public event Action<UnitDragger> DragStarted;
        public event Action<PointerEventData> Dragged;
        public event Action<PointerEventData> DragEnded;

        public void OnBeginDrag(PointerEventData eventData)
        {
            DragStarted?.Invoke(this);
        }

        public void OnDrag(PointerEventData eventData)
        {
            Dragged?.Invoke(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            DragEnded?.Invoke(eventData);
        }
    }
}
