using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconCounter : Counter
{
    [SerializeField] private List<GameObject> _icons;

    public override void Initialize(ICountable stat)
    {
        base.Initialize(stat);
        Enable();
    }

    public override void ChangeValue()
    {
        foreach (var icon in _icons)
            icon.SetActive(false);

        for (int i = 0; i < Stat.Current; i++)
            _icons[i].SetActive(true);
    }
}
