using Battler;
using Battler.UI.BattleView;
using System;
using UnityEngine;

public class DragArmyPanel : SquadPanel<DragItem, BattleSquadCell>
{
    public event Action<DragItem> DragStarted;

    protected override void SubscribeToItem(DragItem item)
    {
        item.DragStarted += OnDragStarted;
    }

    protected override void UnsubscribeFromItem(DragItem item)
    {
        item.DragStarted -= OnDragStarted;
    }

    private void OnDragStarted(DragItem item) => DragStarted?.Invoke(item);
}
