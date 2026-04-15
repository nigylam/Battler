using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    [SerializeField] private List<LevelButton> _levelButtons;
    [SerializeField] private TextCounter _goldCounter;
    [SerializeField] private Button _shopButton;

    public event Action<LevelConfig> Start;
    public event Action Shop;

    private void OnEnable()
    {
        foreach(LevelButton levelButton in _levelButtons)
            levelButton.Clicked += OnLevelClick;

        _shopButton.onClick.AddListener(OnShopClick);
    }

    private void OnDisable()
    {
        foreach (LevelButton levelButton in _levelButtons)
            levelButton.Clicked -= OnLevelClick;

        _shopButton.onClick.RemoveListener(OnShopClick);
    }

    public void Initialize(Gold gold)
    {
        _goldCounter.Initialize(gold);
    }

    private void OnLevelClick(LevelConfig level)
    {
        Start?.Invoke(level);
    }

    private void OnShopClick()
    {
        _shopButton.onClick.RemoveListener(OnShopClick);
        Shop?.Invoke();
    }
}
