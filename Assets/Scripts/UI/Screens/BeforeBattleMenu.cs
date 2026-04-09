using System;
using UnityEngine;
using UnityEngine.UI;

public class BeforeBattleMenu : MonoBehaviour
{
    [SerializeField] private Button _playButton;

    private SquadPlacer _squadPlacer;

    public event Action PlayButtonClicked;

    public void SetPlayButtonActive()
    {
        if (_playButton.gameObject.activeSelf)
            return;

        _playButton.gameObject.SetActive(true);
        _playButton.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        _playButton.onClick.RemoveListener(OnClick);
        _playButton.gameObject.SetActive(false);
    }

    private void OnClick()
    {
        _playButton.onClick.RemoveListener(OnClick);
        PlayButtonClicked?.Invoke();
    }
}
