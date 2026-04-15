using System.Collections.Generic;

public class EnemySide : Side
{
    private List<EnemyRound> _rounds = new();
    private int _currentRound;

    public override void StartLevel(GameContext context)
    {
        _rounds.AddRange(context.Level.Rounds);
        _currentRound = 0;
    }

    protected override void SetRoundAfterPause()
    {
        SetRound(_rounds[_currentRound++]);
    }

    protected override void EndRoundPhase1()
    {
        base.EndRoundPhase1();
        EndRoundPhase2();
    }

    private void SetRound(EnemyRound round)
    {
        foreach (EnemySquad enemySquad in round.Squads)
            CreateSquad(enemySquad.Squad, (enemySquad.PositionX, enemySquad.PositionY));

        RaiseReadyForRound();
    }
}
