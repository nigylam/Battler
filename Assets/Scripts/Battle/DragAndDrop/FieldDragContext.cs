using UnityEngine;

namespace Battler.Battle.DragAndDrop
{
    public class FieldDragContext : DragContext
    {
        public FieldDragContext
        (
            SquadContext squad, 
            DragVisual visual, 
            DragVisualSpawner visualSpawner,
            UnitDragger drager
        ) :base(squad.Plan, visual, visualSpawner, drager)
        {
            Context = squad;
        }

        public SquadContext Context { get; }
    }
}
