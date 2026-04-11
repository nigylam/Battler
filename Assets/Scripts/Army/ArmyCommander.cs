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

        _spawnedSquads.Add(new SquadContext(squad, plan, startCell));
        _army.Add(squad);
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
}
