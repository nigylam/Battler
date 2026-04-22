using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battler
{
    public abstract class SquadCell
    {
        public SquadCell(SquadPlan plan, int count)
        {
            if (plan == null)
                throw new ArgumentNullException(nameof(plan));

            if (count <= 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            Plan = plan;
            Count = count;
        }

        public SquadPlan Plan { get; }
        public int Count { get; private set; }

        public void Decrease()
        {
            Count--;

            if (Count < 0)
                Count = 0;
        }

        public void Increase(int count)
        {
            if (count <= 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            Count += count;
        }
    }
}
