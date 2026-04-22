using System;
using System.Collections.Generic;
using UnityEngine;

namespace Battler
{
    public abstract class SquadKeeper<TSquad> : Keeper<TSquad> where TSquad : SquadCell
    {
        public SquadKeeper(List<TSquad> squads) : base(squads) { }

        public override void AddSquad(TSquad squad)
        {
            if (squad == null)  
                throw new ArgumentNullException(nameof(squad));

            if (Contains(squad, out TSquad containingSquad))
                containingSquad.Increase(squad.Count);
            else
                Add(squad);

            RaiseChanged();
        }

        public override void RemoveSquad(TSquad squad)
        {
            if (squad == null)
                throw new ArgumentNullException(nameof(squad));

            if (Contains(squad, out TSquad squadContext) == false)
                throw new InvalidOperationException(nameof(RemoveSquad));

            if (squadContext.Count == 1)
            {
                Remove(squadContext);
            }
            else
            {
                squadContext.Decrease();
            }

            RaiseChanged();
        }

        protected abstract bool Contains(TSquad squad, out TSquad containingSquad);
    }
}
