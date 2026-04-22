using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battler.UI.SquadView
{
    public interface ISquadViewable<TSquad>
    {
        public event Action Changed;

        public IReadOnlyList<TSquad> Squads { get; }
    }
}
