using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelMenu : MonoBehaviour
{
    [SerializeField] private List<LevelButton> _levelButtons;
    [SerializeField] private TextCounter _goldCounter;

    public event Action<Level> Start;

    private void OnEnable()
    {
        foreach(LevelButton levelButton in _levelButtons)
            levelButton.Clicked += OnClick;
    }

    private void OnDisable()
    {
        foreach (LevelButton levelButton in _levelButtons)
            levelButton.Clicked -= OnClick;
    }

    public void Initialize(Gold gold)
    {
        _goldCounter.Initialize(gold);
    }

    private void OnClick(Level level)
    {
        foreach (LevelButton levelButton in _levelButtons)
            levelButton.Clicked -= OnClick;

        Start?.Invoke(level);
    }
}
