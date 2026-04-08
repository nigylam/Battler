using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Side : MonoBehaviour
{
    [SerializeField] private Army _army;
    [SerializeField] private Field _field;
    [SerializeField] private SquadCreator _squadCreator;

    private float _roundPause = 1f;
    private Coroutine _setRound;
    private List<SquadContext> _spawnedSquads = new();

    public event Action WinRound;
    public event Action SquadCreated;

    private void OnEnable()
    {
        _army.WinRound += OnWinRound;
        _field.CellTaken += OnCellTaken;
        OnOnEnable();
    }

    private void OnDisable()
    {
        _field.CellTaken -= OnCellTaken;
        _army.WinRound -= OnWinRound;
        OnOnDisable();
    }

    public void PrepareToRound()
    {
        ClearAfterRound();

        if (_setRound != null)
            StopCoroutine(_setRound);

        _setRound = StartCoroutine(SetRoundAfterPause());
    }

    public void Attack()
    {
        _army.Attack();
    }

    protected virtual void Restart()
    {
        ClearAfterRound();
        _spawnedSquads.Clear();
    }

    protected abstract void SetRound();
    protected virtual void OnOnEnable() { }
    protected virtual void OnOnDisable() { }

    protected virtual void DoAfterWin(List<SquadContext> survivedSquads) { }

    protected bool TryCreateSquad(SquadPlan plan, (int x, int y) startCell, bool createUpgraded = false)
    {
        if (_squadCreator.TryCreate(plan, startCell, gameObject.transform, _field, createUpgraded, out Squad newSquad))
        {
            _army.AddSquad(newSquad);
            _spawnedSquads.Add(new SquadContext(newSquad, plan, startCell));
            return true;
        }

        return false;
    }

    private void OnCellTaken()
    {
        SquadCreated?.Invoke();
    }

    private void ClearAfterRound()
    {
        _army.Clear();
        _field.Clear();
    }

    private IEnumerator SetRoundAfterPause()
    {
        float time = 0;

        while (time < _roundPause)
        {
            time += Time.deltaTime;
            yield return null;
        }

        SetRound();
    }

    private void OnWinRound()
    {
        DoAfterWin(GetSurvived());
        _spawnedSquads.Clear();
        WinRound?.Invoke();
    }

    private List<SquadContext> GetSurvived()
    {
        List<SquadContext> survivedSquads = new();

        foreach (var squad in _army.AliveSquads)
            foreach (var squadContext in _spawnedSquads)
                if (squad == squadContext.Squad)
                    survivedSquads.Add(squadContext);

        return survivedSquads;
    }
}
