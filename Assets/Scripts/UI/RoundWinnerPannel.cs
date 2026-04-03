using System.Collections;
using TMPro;
using UnityEngine;

public class RoundWinnerPannel : MonoBehaviour
{
    private readonly string EnemyWinnerText = "Enemy win";
    private readonly string PlayerWinnerText = "Player win";

    [SerializeField] private TextMeshProUGUI _title;

    private float _disableTime = 1f;
    private Coroutine _disable;

    public void SetEnemyWinner()
    {
        SetTitle(EnemyWinnerText);
    }

    public void SetPlayerWinner()
    {
        SetTitle(PlayerWinnerText);
    }

    private void SetTitle(string title)
    {
        gameObject.SetActive(true);
        _title.text = title;

        if (_disable != null)
            StopCoroutine(_disable);

        _disable = StartCoroutine(DisableAfterPause());
    }

    private IEnumerator DisableAfterPause()
    {
        float time = 0;

        while (time < _disableTime)
        {
            time += Time.deltaTime;
            yield return null;
        }

        gameObject.SetActive(false);
    }
}
