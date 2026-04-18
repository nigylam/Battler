using Battler;
using Battler.Battle.DragAndDrop;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Timeline.TimelinePlaybackControls;

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

    public override void EndLevel()
    {
        base.EndLevel();
        _placer.Disable();
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
        Commander.DragStarted += OnDragStarted;
    }

    protected override void Disable()
    {
        _placer.PlayButtonClicked -= OnPlayButtonClicked;
        _placer.Place -= OnPlace;
        Commander.SurvivedUpgraded -= OnSurvivedUpgraded;
        Commander.DragStarted -= OnDragStarted;
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

    private void OnDragStarted(SquadContext context, UnitDragger dragger)
    {
        Image icon = Instantiate(context.Plan.DragIcon, context.Squad.transform);
        SquadPreview preview = Instantiate(context.Plan.Preview, context.Squad.transform);
        FieldDragContext fieldDrag = new(context, dragger, icon, preview);
        _placer.StartFieldDrag(fieldDrag);
        SubscribeFieldDrag(fieldDrag);
        Field.Free(context.StartCell, context.Plan.Size);
    }

    private void SubscribeFieldDrag(FieldDragContext fieldDrag)
    {
        fieldDrag.AddItem += AddArmyItem;
        fieldDrag.CanceDrag += CancelDrag;
        fieldDrag.MoveSquad += MoveSquad;
    }

    private void UnsubscribeFieldDrag(FieldDragContext fieldDrag)
    {
        fieldDrag.AddItem -= AddArmyItem;
        fieldDrag.CanceDrag -= CancelDrag;
        fieldDrag.MoveSquad -= MoveSquad;
    }

    private void AddArmyItem(FieldDragContext fieldDrag)
    {
        Commander.Remove(fieldDrag.Context);
        _beforeBattleMenu.Add(fieldDrag.Squad);
        UnsubscribeFieldDrag(fieldDrag);
    }

    private void CancelDrag(FieldDragContext fieldDrag)
    {
        Field.Take(fieldDrag.Context.StartCell, fieldDrag.Context.Plan.Size);
        UnsubscribeFieldDrag(fieldDrag);
    }

    private void MoveSquad(FieldDragContext fieldDrag)
    {
        Commander.Remove(fieldDrag.Context);
        UnsubscribeFieldDrag(fieldDrag);
    }
}
