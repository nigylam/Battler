using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleEndScreen : MonoBehaviour
{
    private readonly string YouLoseText = "You lose";
    private readonly string YouWinText = "You win";

    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private GameObject _rewardBlock;
    [SerializeField] private TextMeshProUGUI _goldReward;
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
        _rewardBlock.SetActive(false);
        _title.text = YouLoseText;
    }

    public void SetWinText(int goldReward)
    {
        _rewardBlock.SetActive(true);
        _title.text = YouWinText;
        _goldReward.text = goldReward.ToString();
    }

    private void OnClick()
    {
        _endButton.onClick.RemoveListener(OnClick);
        End?.Invoke();
    }
}
