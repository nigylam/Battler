using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _startButton;

    public event Action Start;

    private void OnEnable()
    {
        _startButton.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        _startButton.onClick.RemoveListener(OnClick);
    }

    private void OnClick()
    {
        _startButton.onClick.RemoveListener(OnClick);
        Start?.Invoke();
    }
}
