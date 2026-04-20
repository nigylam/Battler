using Battler.Battle.Armies;
using Battler.Battle.DragAndDrop;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ArmyCommander
{
    private readonly Army _army;
    private readonly Field _field;

    private List<SquadContext> _spawnedSquads = new();
    private List<SquadContext> _survivedSquads = new();

    public event Action SurvivedUpgraded;
    public event Action FieldCleared;
    public event Action<SquadContext, UnitDragger> DragStarted;

    public IReadOnlyCollection<SquadContext> SurvivedSquads => _survivedSquads;

    public ArmyCommander(Army army, Field field)
    {
        _army = army;
        _field = field;
    }

    public void Attack()
    {
        _army.Attack();
    }

    public void Clear()
    {
        _army.Clear();
        _field.Clear();
        _survivedSquads.Clear();
        _spawnedSquads.Clear();
    }

    public void ClearField()
    {
        _army.Clear();
        _field.Clear();
        _spawnedSquads.Clear();
        FieldCleared?.Invoke();
    }

    public void Add(Squad squad, SquadPlan plan, (int x, int y) startCell)
    {
        if (squad == null)
            throw new ArgumentNullException(nameof(squad));

        if (plan == null)
            throw new ArgumentNullException(nameof(plan));

        var squadContext = new SquadContext(squad, plan, startCell);
        _spawnedSquads.Add(squadContext);
        _army.Add(squad);

        squad.DragStarted += OnDragStarted;
    }

    public void UpgradeSurvived()
    {
        if (_survivedSquads.Count > 0)
            foreach (SquadContext squad in _survivedSquads)
                squad.Upgrade();

        SurvivedUpgraded?.Invoke();
    }

    public void GetSurvived()
    {
        _survivedSquads.Clear();

        foreach (var squad in _army.AliveSquads)
            foreach (var squadContext in _spawnedSquads)
                if (squad == squadContext.Squad)
                    _survivedSquads.Add(squadContext);
    }

    public void Remove(SquadContext context)
    {
        _spawnedSquads.Remove(context);
        _army.Remove(context.Squad);
    }

    public void UpdateSquadPosition(SquadContext context, (int x, int y) startCell)
    {
        context.UpdatePosition(startCell);
    }

    private void OnDragStarted(Squad squad, UnitDragger dragger)
    {
        DragStarted?.Invoke(Get(squad), dragger);
    }

    private SquadContext Get(Squad squad)
    {
        SquadContext squadContext = null;

        foreach (var context in _spawnedSquads)
        {
            if (context.Squad == squad)
                squadContext = context;
        }

        return squadContext;
    }
}
