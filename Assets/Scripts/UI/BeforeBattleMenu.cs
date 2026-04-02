using System;
using UnityEngine;
using UnityEngine.UI;

public class BeforeBattleMenu : MonoBehaviour
{
    [SerializeField] private Button _playButton;

    public event Action PlayButtonClicked;

    private void OnEnable()
    {
        _playButton.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        _playButton.onClick.RemoveListener(OnClick);
    }

    private void OnClick()
    {
        _playButton.onClick.RemoveListener(OnClick);
        PlayButtonClicked?.Invoke();
    }
}
