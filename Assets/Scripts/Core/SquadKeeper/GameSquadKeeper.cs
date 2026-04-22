using System.Collections.Generic;
using UnityEngine;

namespace Battler.Core.SquadKeeping
{
    public class GameSquadKeeper : SquadKeeper<GameSquadCell>
    {
        public GameSquadKeeper(List<GameSquadCell> squads) : base(squads) { }

        protected override bool Contains(GameSquadCell squad, out GameSquadCell containingSquad)
        {
            containingSquad = null;

            foreach (GameSquadCell squadCell in Squads)
            {
                if (squadCell.Plan == squad.Plan)
                {
                    containingSquad = squadCell;
                    return true;
                }
            }

            return false;
        }
    }
}