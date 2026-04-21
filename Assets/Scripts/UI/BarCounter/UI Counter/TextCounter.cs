using System;
using TMPro;
using UnityEngine;

public class TextCounter : Counter
{
    [SerializeField] private TextMeshProUGUI _text;

    public override void Initialize(ICountable stat)
    {
        base.Initialize(stat);
    }

    public override void ChangeValue()
    {
        int value = (int)Stat.Current;
        _text.text = value.ToString();
    }
}
