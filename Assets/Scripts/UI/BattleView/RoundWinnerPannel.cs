using System.Collections;
using TMPro;
using UnityEngine;

public class RoundWinnerPannel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _titlePlayer;
    [SerializeField] private TextMeshProUGUI _titleEnemy;
    [SerializeField] private TextCounter _roundTextCounter;

    private readonly float _disableTime = 1f;

    private RoundCounter _roundCounter;
    private Coroutine _disable;

    private void Awake()
    {
        _roundCounter = new RoundCounter();
        _roundTextCounter.Initialize(_roundCounter);
    }

    private void OnDisable()
    {
        _roundTextCounter.Disable();
    }

    public void SetEnemyWinner()
    {
        Activate();
        _titlePlayer.gameObject.SetActive(false);
        _titleEnemy.gameObject.SetActive(true);
    }

    public void SetPlayerWinner()
    {
        Activate();
        _titlePlayer.gameObject.SetActive(true);
        _titleEnemy.gameObject.SetActive(false);
    }

    public void Restart()
    {
        _roundCounter.Restart();
    }

    private void Activate()
    {
        gameObject.SetActive(true);
        _roundCounter.Increase();
        _roundTextCounter.Enable();
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
