using Battler.UI.BattleView;
using System;
using UnityEngine;

public class DragArmyPanel : SquadPanel<DragItem, SquadData>
{
    public event Action<DragItem> DragStarted;

    public void Add(SquadPlan squadPlan)
    {
        if (squadPlan == null)
            throw new ArgumentNullException(nameof(squadPlan));

        bool haveItem = false;

        foreach (DragItem item in Items)
        {
            if (item.SquadPlan == squadPlan)
            {
                haveItem = true;
                item.gameObject.SetActive(true);
                item.Increase();
            }
        }

        if (haveItem == false)
        {
            SquadData data = new SquadData(squadPlan, 1);
            AddItem(data);
        }
    }

    public void Remove(SquadPlan squadPlan)
    {
        if (squadPlan == null)
            throw new ArgumentNullException(nameof(squadPlan));

        bool haveItem = false;

        foreach (DragItem item in Items)
        {
            if (item.SquadPlan == squadPlan)
            {
                haveItem = true;
                item.Decrease();
            }
        }

        if (haveItem == false)
            throw new InvalidOperationException(nameof(Remove));
    }

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
