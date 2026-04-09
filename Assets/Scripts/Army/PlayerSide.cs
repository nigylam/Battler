using System.Collections.Generic;
using UnityEngine;

public class PlayerSide : Side
{
    [SerializeField] private SquadPlacer _placer;
    [SerializeField] private BeforeBattleMenu _beforeBattleMenu;

    private List<SquadContext> _survived = new();
    private bool _firsSquadCreated;

    public void SetSquads(SquadKeeper squadKeeper)
    {
        Restart();
        _placer.SetSquads(squadKeeper);
    }

    public void OnLevelEnd()
    {
        _placer.ClearSquads();
    }

    protected override void Enable()
    {
        _placer.ReadyForBuild += OnReadyForBuild;
        _beforeBattleMenu.PlayButtonClicked += OnPlayButtonClicked;
    }

    protected override void Disable()
    {
        _placer.ReadyForBuild -= OnReadyForBuild;
        _beforeBattleMenu.PlayButtonClicked -= OnPlayButtonClicked;
    }

    protected override void OnRoundEnd()
    {
        base.OnRoundEnd();
    }

    protected override void Restart()
    {
        base.Restart();
        _survived.Clear();
    }

    protected override void SetRoundBeforePause()
    {
        _firsSquadCreated = false;
        _beforeBattleMenu.gameObject.SetActive(true);
    }

    protected override void SetRoundAfterPause()
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

    private void OnReadyForBuild(SquadPlan plan, (int x, int y) startCell)
    {
        CreateSquad(plan, startCell);
    }

    private void CreateSquad(SquadPlan plan, (int x, int y) startCell, bool createUpgraded = false)
    {
        TryCreateSquad(plan, startCell, createUpgraded);

        if (_firsSquadCreated)
            return;

        _beforeBattleMenu.SetPlayButtonActive();
        _firsSquadCreated = true;
    }

    private void OnPlayButtonClicked()
    {
        RaiseReadyForRound();
        _beforeBattleMenu.gameObject.SetActive(false);
    }

    private void RespawnSurvivedSquads()
    {
        if (_survived.Count == 0)
            return;

        foreach (SquadContext squadContext in _survived)
            CreateSquad(squadContext.Plan, squadContext.StartCell, squadContext.CreateUpgraded);

        _survived.Clear();
    }
}
