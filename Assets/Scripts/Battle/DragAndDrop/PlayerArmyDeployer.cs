using Battler.Battle.Armies;
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

        public PlayerArmyDeployer
        (
            BeforeBattleMenu menu, 
            SquadPlacer placement, 
            Field field, 
            ArmyCommander commander, 
            DragVisualSpawner visualSpawner, 
            SquadCreator creator, 
            Transform squadsParrent
        ) : base(field, commander, creator, squadsParrent)
        {
            _menu = menu;
            _placer = placement;
            _visualSpawner = visualSpawner;
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
        }

        public void DisablePlacing()
        {
            _menu.DragStarted -= OnMenuDragStarted;
            _menu.PlayButtonClicked -= OnPlayClicked;
            Commander.DragStarted -= OnFieldDragStarted;
            _placer.DropSuccess -= OnDropSuccess;
            _placer.DropFail -= OnDropFail;
            _placer.DropToUI -= OnDropToUI;
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
            DragVisual visuals = _visualSpawner.Spawn(uiItem.Squad);
            MenuDragContext menuDrag = new (uiItem, visuals, _visualSpawner);
            _placer.StartDrag(menuDrag);
        }

        private void OnFieldDragStarted(SquadContext context, UnitDragger dragger)
        {
            Field.Free(context.StartCell, context.Plan.Size);
            DragVisual visual = _visualSpawner.Spawn(context.Plan);
            FieldDragContext fieldDrag = new FieldDragContext(context, dragger, visual, _visualSpawner);
            _placer.StartDrag(fieldDrag);
        }

        private void ValidatePlayButton()
        {
            _menu.SetPlayButtonActive();
        }

        private void OnDropSuccess(IPlacementDrag item, (int x, int y) startCell)
        {
            item.ConfirmPlacement();

            if (item is FieldDragContext fieldItem)
            {
                Commander.UpdateSquadPosition(fieldItem.Context, startCell);
                Creator.ChangePlace(fieldItem.Context.Plan, fieldItem.Context.Squad, startCell, Field);
            }
            else
            {
                CreateSquad(item.Squad, startCell);
            }

            ValidatePlayButton();
        }

        private void OnDropFail(IPlacementDrag item)
        {
            item.CancelPlacement();

            if (item is FieldDragContext fieldItem)
            {
                Field.Take(fieldItem.Context.StartCell, item.Squad.Size);
            }
        }

        private void OnDropToUI(IPlacementDrag item)
        {
            item.CancelPlacement();

            if (item is FieldDragContext fieldItem)
            {
                Commander.Remove(fieldItem.Context);
                _menu.Add(item.Squad);
            }
        }

        private void OnPlayClicked() => RaiseDeploymentFinished();
    }
}
