using System;
using UnityEngine.EventSystems;

namespace Battler.BattleSystem.DragAndDrop
{
    public interface IDragable
    {
        public event Action<PointerEventData> Dragged;
        public event Action<PointerEventData> DragEnded;
    }
}
