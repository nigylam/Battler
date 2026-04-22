using Battler.Battle.Armies;
using Battler.Battle.Squads;
using Battler.UI.BattleView;
using System;
using UnityEngine;

namespace Battler.Battle.DragAndDrop
{
    public class PlayerArmyDeployer : ArmyDeployer
    {
        private readonly BeforeBattleMenu _menu;
        private readonly SquadPlacer _placer;
        private readonly DragVisualSpawner _visualSpawner;

        private BattleSquadKeeper _keeper;

        public PlayerArmyDeployer
        (
            BeforeBattleMenu menu, 
            SquadPlacer placer, 
            Field field, 
            ArmyCommander commander, 
            DragVisualSpawner visualSpawner, 
            SquadCreator creator, 
            Transform squadsParrent
        ) : base(field, commander, creator, squadsParrent)
        {
            _menu = menu;
            _placer = placer;
            _visualSpawner = visualSpawner;
        }

        public void Set(BattleSquadKeeper keeper)
        {
            _keeper = keeper;
        }

        public void EnablePlacing()
        {
            _menu.gameObject.SetActive(true);
            _menu.DragStarted += OnMenuDragStarted;
            _menu.PlayButtonClicked += OnPlayClicked;
            Commander.DragStarted += OnFieldDragStarted;
            _placer.DropSuccess += OnDropSuccess;
            _placer.DropFail += OnDropFail;
            _placer.DropToUI += OnDropToUI;
            _placer.UIHover += OnUIHover;
            _placer.WorldHover += OnWorldHover;
        }

        public void DisablePlacing()
        {
            _menu.DragStarted -= OnMenuDragStarted;
            _menu.PlayButtonClicked -= OnPlayClicked;
            Commander.DragStarted -= OnFieldDragStarted;
            _placer.DropSuccess -= OnDropSuccess;
            _placer.DropFail -= OnDropFail;
            _placer.DropToUI -= OnDropToUI;
            _placer.WorldHover -= OnWorldHover;
            _menu.gameObject.SetActive(false);
        }

        public void RespawnSurvived()
        {
            if (Commander.SurvivedSquads.Count == 0)
                return;

            foreach (var squadContext in Commander.SurvivedSquads)
                CreateSquad(squadContext.Plan, squadContext.StartCell, squadContext.CreateUpgraded);

            ValidatePlayButton();
        }

        private void OnMenuDragStarted(DragItem uiItem)
        {
            DragVisual visuals = _visualSpawner.Spawn(uiItem.SquadPlan);
            MenuDragContext menuDrag = new (uiItem, visuals, _visualSpawner);
            _placer.StartDrag(menuDrag);
        }

        private void OnFieldDragStarted(SquadFieldContext context, UnitDragger dragger)
        {
            context.Squad.HideVisuals();
            Field.Free(context.StartCell, context.Plan.Size);
            DragVisual visual = _visualSpawner.Spawn(context.Plan);
            FieldDragContext fieldDrag = new (context, visual, _visualSpawner, dragger);
            _placer.StartDrag(fieldDrag);
        }

        private void ValidatePlayButton()
        {
            _menu.SetPlayButtonActive();
        }

        private void OnDropSuccess(DragContext item, (int x, int y) startCell)
        {
            _menu.SetPlacingUnavailable();
            item.Dispose();

            if (item is FieldDragContext fieldItem)
            {
                fieldItem.Context.Squad.ShowVisuals();
                Commander.UpdateSquadPosition(fieldItem.Context, startCell);
                Creator.ChangePlace(fieldItem.Context.Plan, fieldItem.Context.Squad, startCell, Field);
            }
            else
            {
                _keeper.RemoveSquad(new BattleSquadCell(item.SquadPlan, 1, item.CreateUpgraded));
                CreateSquad(item.SquadPlan, startCell, item.CreateUpgraded);
            }

            ValidatePlayButton();
        }

        private void OnDropFail(DragContext item)
        {
            _menu.SetPlacingUnavailable();
            item.Dispose();

            if (item is FieldDragContext fieldItem)
            {
                fieldItem.Context.Squad.ShowVisuals();
                Field.Take(fieldItem.Context.StartCell, item.SquadPlan.Size);
            }
        }

        private void OnDropToUI(DragContext item)
        {
            _menu.SetPlacingUnavailable();
            item.Dispose();

            if (item is FieldDragContext fieldItem)
            {
                Commander.Remove(fieldItem.Context);
                _keeper.AddSquad(new BattleSquadCell(item.SquadPlan, 1, item.CreateUpgraded));
            }
        }

        private void OnUIHover(DragContext item)
        {
            if (item is FieldDragContext)
                _menu.SetPlacingAvailable();
        }

        private void OnWorldHover(DragContext context)
        {
            _menu.SetPlacingUnavailable();
        }

        private void OnPlayClicked() => RaiseDeploymentFinished();
    }
}