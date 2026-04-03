using UnityEngine;

public class PlayerSide : Side
{
    [SerializeField] private SquadPlacer _placer;

    protected override void OnOnEnable()
    {
        _placer.ReadyForBuild += OnReadyForBuild;
    }

    private void OnReadyForBuild(SquadPlan plan, (int x, int y) startCell)
    {
        TryCreateSquad(plan, startCell);
    }

    protected override void OnOnDisable()
    {
        _placer.ReadyForBuild -= OnReadyForBuild;
    }

    protected override void SetRound()
    {
        RespawnSurvivedSquads();
    }

    private void RespawnSurvivedSquads()
    {
        if (SurvivedSquads.Count == 0)
            return;

        foreach (SquadContext squadContext in SurvivedSquads)
            TryCreateSquad(squadContext.Plan, squadContext.StartCell);
    }
}
