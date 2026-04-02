using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Side : MonoBehaviour
{
    [SerializeField] private Army _army;
    [SerializeField] private WinsCounter _winsCounter;
    [SerializeField] private IconCounter _iconWinsCounter;
    [SerializeField] private Field _field;
    [SerializeField] private SquadCreator _squadCreator;
    [SerializeField] private int _roundsToWin;

    private List<SquadContext> _spawnedSquads = new();
    private List<SquadContext> _survivedSquads = new();

    public event Action WinBattle;
    public event Action WinRound;

    public IReadOnlyCollection<SquadContext> SurvivedSquads => _survivedSquads;

    private void OnEnable()
    {
        _army.WinRound += OnWinRound;
        _winsCounter.Win += OnWinBattle;
        _winsCounter.Initialize(_roundsToWin);
        _iconWinsCounter.Initialize(_winsCounter);
        OnOnEnable();
    }

    private void OnDisable()
    {
        _army.WinRound -= OnWinRound;
        _winsCounter.Win -= OnWinBattle;
        OnOnDisable();
    }

    public abstract void PrepareToRound();

    public void Attack()
    {
        _army.Attack();
    }

    protected virtual void OnOnEnable() { }
    protected virtual void OnOnDisable() { }

    protected bool TryCreateSquad(SquadPlan plan, (int x, int y) startCell)
    {
        if (_squadCreator.TryCreate(plan, startCell, gameObject.transform, _field, out Squad newSquad))
        {
            _army.AddSquad(newSquad);
            _spawnedSquads.Add(new SquadContext(newSquad, plan, startCell));
            return true;
        }

        return false;
    }

    protected void ClearAfterRound()
    {
        _army.Clear();
        _field.Clear();
    }

    private void OnWinRound()
    {
        GetSurvived();
        _spawnedSquads.Clear();
        _winsCounter.Increase();
        WinRound?.Invoke();
    }

    private void GetSurvived()
    {
        List<Squad> survivedSquads = new();
        survivedSquads.AddRange(_army.AliveSquads);

        foreach(var squad in survivedSquads)
            foreach(var squadContext in _spawnedSquads)
                if(squad == squadContext.Squad)
                    _survivedSquads.Add(squadContext);
    }

    private void OnWinBattle()
    {
        WinBattle?.Invoke();
    }
}
