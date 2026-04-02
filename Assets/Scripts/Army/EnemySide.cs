using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySide : Side
{
    [SerializeField] private EnemyLevel _level;
    [SerializeField] private ArmyFabric _fabric;

    private List<EnemyRound> _rounds = new();
    private int _currentRound = 0;

    private void Awake()
    {
        _rounds.AddRange(_level.Rounds);
    }

    public override void PrepareToRound()
    {
        if (_currentRound != 0)
        {
            Army.Clear();
            Field.Clear();
        }

        _fabric.SetRound(_rounds[_currentRound++]);
    }
}
