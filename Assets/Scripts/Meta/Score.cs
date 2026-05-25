using System;
using UnityEngine;
using YG;

namespace Battler.Meta
{
    public class Score : ICountable
    {
        private int _count;

        public Score()
        {
            _count = YG2.saves.score;
        }

        public float Max => 9999;

        public float Current
        {
            get
            {
                return _count;
            }
            private set
            {
                _count = (int)value;
                YG2.saves.score = (int)Current;
                Changed?.Invoke();
            }
        }

        public event Action Changed;

        public void Increase(int count)
        {
            _count += count;
            YG2.SetLeaderboard("test", _count);
        }
    }
}
