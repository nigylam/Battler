using System.Collections;
using TMPro;
using UnityEngine;

public class RoundWinnerPannel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _titlePlayer;
    [SerializeField] private TextMeshProUGUI _titleEnemy;

    private float _disableTime = 1f;
    private Coroutine _disable;

    public void SetEnemyWinner()
    {
        gameObject.SetActive(true);
        _titlePlayer.gameObject.SetActive(false);
        _titleEnemy.gameObject.SetActive(true);
        SetCoroutine();
    }

    public void SetPlayerWinner()
    {
        gameObject.SetActive(true);
        _titlePlayer.gameObject.SetActive(true);
        _titleEnemy.gameObject.SetActive(false);
        SetCoroutine();
    }

    private void SetCoroutine()
    {
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
