using System;
using UnityEngine;

public abstract class Side : MonoBehaviour
{
    [SerializeField] protected Army _army;
    [SerializeField] private WinsCounter _winsCounter;
    [SerializeField] private IconCounter _iconWinsCounter;
    [SerializeField] private Field _field;
    [SerializeField] private SquadCreator _squadCreator;
    [SerializeField] private int _roundsToWin;

    public event Action WinBattle;
    public event Action WinRound;

    protected Army Army => _army;
    protected Field Field => _field;
    protected SquadCreator SquadCreator => _squadCreator;

    private void OnEnable()
    {
        _army.WinRound += OnWinRound;
        _winsCounter.Win += OnWinBattle;
        _winsCounter.Initialize(_roundsToWin);
        _iconWinsCounter.Initialize(_winsCounter);
        OnOnEnable();
    }

    public abstract void PrepareToRound();

    public void Attack()
    {
        _army.Attack();
    }

    protected virtual void OnOnEnable() { }

    protected virtual void OnWinRound()
    {
        _winsCounter.Increase();
        WinRound?.Invoke();
    }

    private void OnWinBattle()
    {
        WinBattle?.Invoke();
    }
}
