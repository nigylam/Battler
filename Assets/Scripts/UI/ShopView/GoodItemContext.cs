using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battler
{
    public readonly struct GoodItemContext
    {
        public readonly SquadGood SquadGood;
        public readonly bool CanAfford;

        public GoodItemContext(SquadGood squadGood, bool canAfford)
        {
            SquadGood = squadGood;
            CanAfford = canAfford;
        }
    }
}
