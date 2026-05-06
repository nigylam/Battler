using Battler.BattleSystem.Squads;
using UnityEngine;

namespace Battler.BattleSystem.DragAndDrop
{
    public class FieldDragContext : DragContext
    {
        public FieldDragContext
        (
            SquadFieldContext squadContext, 
            DragVisual visual, 
            DragVisualSpawner visualSpawner,
            UnitDragger drager
        ) :base(squadContext.Plan, squadContext.CreateUpgraded, visual, visualSpawner, drager)
        {
            Context = squadContext;
        }

        public SquadFieldContext Context { get; }
    }
}
