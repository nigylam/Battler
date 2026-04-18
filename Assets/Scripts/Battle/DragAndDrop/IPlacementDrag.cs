using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Battler
{
    public interface IPlacementDrag
    {
        SquadPlan Squad { get; }

        event Action<PointerEventData> Dragged;
        event Action<PointerEventData> DragEnded;

        public void HandleUIDrag(PointerEventData eventData);
        public void HandleWorldDrag();
        public void HandleBuildAvailable(Vector3 position);
        public void HandleBuildBlocked(Vector3 position);
        public void ConfirmPlacement();
        public void CancelPlacement();
    }
}
