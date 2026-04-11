using UnityEngine;

public class PlayerSide : Side
{
    [SerializeField] private BeforeBattleMenu _beforeBattleMenu;
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private LayerMask _thisCanvas;

    private SquadPlacer _placer;

    public override void StartLevel(GameContext context)
    {
        _placer.SetSquads(context.SquadKeeper);
    }

    protected override void OnAwake()
    {
        _placer = new SquadPlacer(_beforeBattleMenu, _camera, _groundMask, _thisCanvas);
    }

    protected override void Enable()
    {
        _placer.PlayButtonClicked += OnPlayButtonClicked;
        _placer.Place += OnPlace;
        Commander.SurvivedUpgraded += OnSurvivedUpgraded;
    }

    protected override void Disable()
    {
        _placer.PlayButtonClicked -= OnPlayButtonClicked;
        _placer.Place -= OnPlace;
        Commander.SurvivedUpgraded -= OnSurvivedUpgraded;
    }

    protected override void SetRoundBeforePause()
    {
        _placer.Enable();
    }

    protected override void SetRoundAfterPause()
    {
        RespawnSurvived();
    }   

    protected override void EndRoundPhase1()
    {
        base.EndRoundPhase1();
        Commander.UpgradeSurvived();
    }

    private void OnPlace(SquadPlan plan, (int x, int y) startCell)
    {
        CreateSquad(plan, startCell);
        _beforeBattleMenu.SetPlayButtonActive();
    }

    private void OnPlayButtonClicked()
    {
        _placer.Disable();
        RaiseReadyForRound();
    }

    private void RespawnSurvived()
    {
        if (Commander.SurvivedSquads.Count == 0)
            return;

        foreach(var squadContext in Commander.SurvivedSquads)
            CreateSquad(squadContext.Plan, squadContext.StartCell, squadContext.CreateUpgraded);

        _beforeBattleMenu.SetPlayButtonActive();
    }

    private void OnSurvivedUpgraded()
    {
        EndRoundPhase2();
    }
}
