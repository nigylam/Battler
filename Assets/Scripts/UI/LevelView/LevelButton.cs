using Battler.UI;
using System;
using UnityEngine;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private UIButton _button;
    [SerializeField] private LevelConfig _level;

    public event Action<LevelConfig> Clicked;

    private void OnEnable()
    {
        _button.Clicked += OnClick;
    }

    private void OnDisable()
    {
        _button.Clicked -= OnClick;
    }

    private void OnClick()
    {
        _button.Clicked -= OnClick;
        Clicked?.Invoke(_level);
    }
}
