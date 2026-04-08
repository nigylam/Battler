using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSide : Side
{
    [SerializeField] private SquadPlacer _placer;

    private List<SquadContext> _survived = new();

    public void SetSquads(SquadKeeper squadKeeper)
    {
        Restart();
        _placer.SetSquads(squadKeeper);
    }

    public void OnEndLevel()
    {
        _placer.ClearSquads();
    }
    
    protected override void Restart()
    {
        base.Restart();
        _survived.Clear();
    }

    protected override void OnOnEnable()
    {
        _placer.ReadyForBuild += OnReadyForBuild;
    }

    protected override void OnOnDisable()
    {
        _placer.ReadyForBuild -= OnReadyForBuild;
    }

    private void OnReadyForBuild(SquadPlan plan, (int x, int y) startCell)
    {
        TryCreateSquad(plan, startCell);
    }

    protected override void SetRound()
    {
        RespawnSurvivedSquads();
    }

    protected override void DoAfterWin(List<SquadContext> survivedSquads)
    {
        if (survivedSquads.Count == 0)
            return;

        foreach (SquadContext squadContext in survivedSquads)
        {
            squadContext.Upgrade();
            _survived.Add(squadContext);
        }
    }

    private void RespawnSurvivedSquads()
    {
        if (_survived.Count == 0)
            return;

        foreach (SquadContext squadContext in _survived)
            TryCreateSquad(squadContext.Plan, squadContext.StartCell, squadContext.CreateUpgraded);

        _survived.Clear();
    }
}
