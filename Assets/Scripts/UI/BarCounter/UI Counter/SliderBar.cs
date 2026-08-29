using System;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Slider))]
public class SliderBar : Counter
{
    protected Slider Slider;

    public override void Initialize(ICountable stat)
    {
        base.Initialize(stat);
        Slider = GetComponent<Slider>();
    }

    public override void ChangeValue()
    {
        float currentValue = Convert.ToSingle(Stat.Current) / Stat.Max;
        Slider.SetValueWithoutNotify(currentValue);
    }
}
