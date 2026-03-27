using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [SerializeField] Army _playerArmy;
    [SerializeField] Army _enemyArmy;
    [SerializeField] Button _playButton;

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
        _playerArmy.Attack();
        _enemyArmy.Attack();
    }
}
