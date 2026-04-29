using System;
using System.Collections.Generic;
using UnityEngine;

namespace Battler.UI.BattleView
{
    public class IconCounter : Counter
    {
        [SerializeField] private List<WinIcon> _icons;
        [SerializeField] private Transform _iconParrent;

        public override void Initialize(ICountable stat)
        {
            base.Initialize(stat);

            foreach (WinIcon icon in _icons)
                icon.SetDefault();

            Enable();
        }

        public override void ChangeValue()
        {
            for (int i = 0; i < Stat.Current; i++)
                _icons[i].SetComplete();
        }
    }
}