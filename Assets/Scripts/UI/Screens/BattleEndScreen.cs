using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleEndScreen : MonoBehaviour
{
    private readonly string YouLoseText = "You lose";
    private readonly string YouWinText = "You win";

    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private Button _endButton;

    public event Action End;

    private void OnEnable()
    {
        _endButton.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        _endButton.onClick.RemoveListener(OnClick);
    }

    public void SetLoseText()
    {
        _title.text = YouLoseText;
    }

    public void SetWinText()
    {
        _title.text = YouWinText;
    }

    private void OnClick()
    {
        _endButton.onClick.RemoveListener(OnClick);
        End?.Invoke();
    }
}
