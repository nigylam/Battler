using Battler.BattleSystem.Squads;
using Battler.UI.BattleView;

namespace Battler.BattleSystem.DragAndDrop
{
    public class MenuDragContext : DragContext
    {
        private readonly DragItem _dragItem;

        public MenuDragContext
        (
            DragItem sourceItem,
            DragVisual visual,
            DragVisualSpawner spawner
        ) : base(sourceItem.SquadPlan, sourceItem.CreateUpgraded, visual, spawner, sourceItem)
        {
            _dragItem = sourceItem;
        }

        public SquadPlan Squad { get; }
    }
}
