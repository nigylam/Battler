using Battler;
using Battler.UI.BattleView;
using Battler.UI.SquadView;
using System;
using UnityEngine.EventSystems;
using UnityEngine;

public class DragArmyPanel : SquadPanel<DragItem, BattleSquadCell>
{
    public event Action<DragItem> DragStarted;

    protected override void SubscribeToItem(DragItem item)
    {
        item.DragStarted += OnDragStarted;
        item.DragEnded += OnDragEnded;
    }

    protected override void UnsubscribeFromItem(DragItem item)
    {
        item.DragStarted -= OnDragStarted;
        item.DragEnded -= OnDragEnded;
    }

    private void OnDragStarted(DragItem item)
    {
        DragStarted?.Invoke(item);

        foreach(DragItem newItem in Items)
            newItem.SetInteractable(false);
    }

    private void OnDragEnded(PointerEventData _)
    {
        foreach (DragItem item in Items)
            item.SetInteractable(true);
    }
    
}
