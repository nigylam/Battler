using System.Collections.Generic;
using UnityEngine;

public class EnemySide : Side
{
    [SerializeField] private EnemyLevel _level;

    private List<EnemyRound> _rounds = new();
    private int _currentRound = 0;


    private void Awake()
    {
        _rounds.AddRange(_level.Rounds);
    }

    protected override void SetRound()
    {
        SetRound(_rounds[_currentRound++]);
    }

    private void SetRound(EnemyRound round)
    {
        foreach (EnemySquad enemySquad in round.Squads)
            TryCreateSquad(enemySquad.Squad, (enemySquad.PositionX, enemySquad.PositionY));
    }
}
