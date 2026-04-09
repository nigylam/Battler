using System.Collections.Generic;
using UnityEngine;

public class EnemySide : Side
{
    private List<EnemyRound> _rounds = new();
    private int _currentRound;

    public void SetRounds(IReadOnlyCollection<EnemyRound> rounds)
    {
        _rounds.AddRange(rounds);
        _currentRound = 0;
    }

    protected override void SetRoundAfterPause()
    {
        SetRound(_rounds[_currentRound++]);
    }

    private void SetRound(EnemyRound round)
    {
        foreach (EnemySquad enemySquad in round.Squads)
            TryCreateSquad(enemySquad.Squad, (enemySquad.PositionX, enemySquad.PositionY));

        RaiseReadyForRound();
    }
}
