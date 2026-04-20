using Battler.UI.BattleView;

namespace Battler.Battle.DragAndDrop
{
    public class MenuDragContext : DragContext
    {
        private readonly DragItem _dragItem;

        public MenuDragContext
        (
            DragItem sourceItem,
            DragVisual visual,
            DragVisualSpawner spawner
        ) : base(sourceItem.Squad, visual, spawner, sourceItem)
        {
            _dragItem = sourceItem;
        }

        public SquadPlan Squad { get; }

        public override void ConfirmPlacement()
        {
            _dragItem.Decrease();
            base.ConfirmPlacement();
        }
    }
}
