using Battler.Battle.Squads;
using Battler.Core.SquadKeeping;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Battler.Battle.DragAndDrop
{
    public class BattleSquadKeeper : SquadKeeper<BattleSquadCell>
    {
        public BattleSquadKeeper(GameSquadKeeper keeper) : base(GetCells(keeper)) { }

        protected override bool Contains(BattleSquadCell squad, out BattleSquadCell containingSquad)
        {
            containingSquad = null;

            foreach (BattleSquadCell squadContext in Squads)
            {
                if(squadContext.Plan == squad.Plan)
                {
                    if(squadContext.CreateUpgraded == squad.CreateUpgraded)
                    {
                        containingSquad = squadContext;
                        return true;
                    }
                }
            }

            return false;
        }

        private static List<BattleSquadCell> GetCells(GameSquadKeeper keeper)
        {
            List<BattleSquadCell> battleSquadCells = new();

            foreach (GameSquadCell squadCell in keeper.Squads)
                battleSquadCells.Add(new BattleSquadCell(squadCell.Plan, squadCell.Count));

            return battleSquadCells;
        }
    }
}
