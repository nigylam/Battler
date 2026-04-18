using Battler;
using System;

public class DragArmyPanel : SquadPanel<DragItem, SquadData>
{
    public event Action<IPlacementDrag> DragStarted;

    protected override void SubscribeToItem(DragItem item)
    {
        item.DragStarted += OnDragStarted;
    }

    protected override void UnsubscribeFromItem(DragItem item)
    {
        item.DragStarted -= OnDragStarted;
    }

    private void OnDragStarted(IPlacementDrag item) => DragStarted?.Invoke(item);
}
