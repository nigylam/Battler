using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battler
{
    public readonly struct GoodContext
    {
        public readonly SquadGood SquadGood;
        public readonly ItemState State;

        public enum ItemState
        {
            CanBuy,
            NotEnoughMoney,
            ClosedLevel
        }

        public GoodContext(SquadGood squadGood, ItemState state)
        {
            SquadGood = squadGood;
            State = state;
        }
    }
}
