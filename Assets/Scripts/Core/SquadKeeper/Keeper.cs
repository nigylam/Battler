using Battler.UI.SquadView;
using System;
using System.Collections.Generic;

namespace Battler
{
    public abstract class Keeper<TSquad> : ISquadViewable<TSquad>
    {
        private List<TSquad> _squads;

        public event Action Changed;

        public IReadOnlyList<TSquad> Squads => _squads;
        public bool IsEmpty => _squads.Count == 0;

        public Keeper(List<TSquad> squads)
        {
            _squads = squads;

            if (squads == null)
                throw new ArgumentNullException(nameof(squads));

            foreach(var item in squads)
                if(item == null)
                    throw new ArgumentNullException(nameof(squads));
        }

        public abstract void AddSquad(TSquad squad);
        public abstract void RemoveSquad(TSquad squad);

        protected void RaiseChanged()
            => Changed?.Invoke();

        protected void Add(TSquad squad)
            => _squads.Add(squad);

        protected void Remove(TSquad squad)
            => _squads.Remove(squad);
    }
}
