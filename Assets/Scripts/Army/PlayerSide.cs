using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSide : Side
{
    [SerializeField] private SquadPlacer _placer;

    private List<Squad> _survivedSquads = new();
    private Dictionary<Squad, (SquadPlan plan, (int x, int y) startCell)> _spawnedSquads = new();

    public override void PrepareToRound()
    {
        if (_survivedSquads.Count == 0)
        {
            Debug.Log("spqni");
            return;

        }

        Army.Clear();
        Field.Clear();
        Dictionary<Squad, (SquadPlan plan, (int x, int y) startCell)> newSquads = new();

        foreach (Squad squad in _survivedSquads)
        {

            if (SquadCreator.TryCreate(_spawnedSquads[squad].plan, _spawnedSquads[squad].startCell, gameObject.transform, out Squad newSquad))
            {
                Army.AddSquad(newSquad);
                newSquads.Add(newSquad, (_spawnedSquads[squad].plan, _spawnedSquads[squad].startCell));
            }
        }

        _spawnedSquads.Clear();
        _spawnedSquads.AddRange(newSquads);
        _survivedSquads.Clear();
    }

    protected override void OnOnEnable()
    {
        _placer.Spawned += OnSpawned;
    }

    protected override void OnWinRound()
    {
        _survivedSquads.AddRange(Army.AliveSquads);
        base.OnWinRound();
    }

    private void OnSpawned(SquadPlan plan, (int x, int y) startCell, Squad squad)
    {
        _spawnedSquads.Add(squad, (plan, startCell));
    }
}
