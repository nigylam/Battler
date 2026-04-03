using TMPro;
using UnityEngine;

public class BattleEndScreen : MonoBehaviour
{
    private readonly string YouLoseText = "You lose";
    private readonly string YouWinText = "You win";

    [SerializeField] private TextMeshProUGUI _title;

    public void SetLoseText()
    {
        gameObject.SetActive(true);
        _title.text = YouLoseText;
    }

    public void SetWinText()
    {
        gameObject.SetActive(true);
        _title.text = YouWinText;
    }
}
