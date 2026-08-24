using Battler.BattleSystem.Armies;
using Battler.BattleSystem.Squads;
using Battler.UI.BattleView;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Battler.BattleSystem.DragAndDrop
{
    public class PlayerArmyDeployer : ArmyDeployer
    {
        private readonly BattleMenu _menu;
        private readonly SquadPlacer _placer;
        private readonly DragVisualSpawner _visualSpawner;
        private readonly AudioSource _audioSource;
        private readonly AudioClip _startDragSound;
        private readonly AudioClip _menuPlaceSound;

        private BattleSquadKeeper _keeper;

        public event Action SquadsEnded;

        public PlayerArmyDeployer
        (
            BattleMenu menu,
            SquadPlacer placer,
            Field field,
            ArmyCommander commander,
            DragVisualSpawner visualSpawner,
            SquadCreator creator,
            Transform squadsParrent,
            AudioSource audioSource,
            AudioClip startDragSound,
            AudioClip menuPlaceSound
        ) : base(field, commander, creator, squadsParrent)
        {
            _menu = menu;
            _placer = placer;
            _visualSpawner = visualSpawner;
            _audioSource = audioSource;
            _startDragSound = startDragSound;
            _menuPlaceSound = menuPlaceSound;
        }

        public void Set(BattleSquadKeeper keeper)
        {
            _keeper = keeper;
        }

        public void EnablePlacing()
        {
            if (_keeper.IsEmpty && Field.IsEmpty())
                SquadsEnded?.Invoke();

            _menu.ArmyPannel.gameObject.SetActive(true);
            _menu.ArmyPannel.DragStarted += OnMenuDragStarted;
            _menu.StartButtonClicked += OnPlayClicked;
            Commander.DragStarted += OnFieldDragStarted;
            _placer.DropSuccess += OnDropSuccess;
            _placer.DropFail += OnDropFail;
            _placer.DropToUI += OnDropToUI;
            _placer.UIHover += OnUIHover;
            _placer.WorldHover += OnWorldHover;
        }

        public void DisablePlacing()
        {
            _menu.ArmyPannel.DragStarted -= OnMenuDragStarted;
            _menu.StartButtonClicked -= OnPlayClicked;
            Commander.DragStarted -= OnFieldDragStarted;
            _placer.DropSuccess -= OnDropSuccess;
            _placer.DropFail -= OnDropFail;
            _placer.DropToUI -= OnDropToUI;
            _placer.WorldHover -= OnWorldHover;
            _menu.ArmyPannel.gameObject.SetActive(false);
        }

        public async UniTask RespawnSurvived()
        {
            await Spawn(Commander.SurvivedSquads);
            ResetPlayButton();
        }

        private void ResetPlayButton()
        {
            if (Field.IsEmpty() == false)
                _menu.EnablePlayButton();
            else
                _menu.DisablePlayButton();
        }

        private void OnMenuDragStarted(DragItem uiItem)
        {
            DragVisual visuals = _visualSpawner.Spawn(uiItem.SquadPlan);
            MenuDragContext menuDrag = new(uiItem, visuals, _visualSpawner);
            _placer.StartDrag(menuDrag);
            _audioSource.PlayOneShot(_startDragSound);
        }

        private void OnFieldDragStarted(SquadFieldContext context, UnitDragger dragger)
        {
            context.Squad.HideVisuals();
            Field.Free(context.StartCell, context.Plan.Size);
            DragVisual visual = _visualSpawner.Spawn(context.Plan);
            FieldDragContext fieldDrag = new(context, visual, _visualSpawner, dragger);
            _placer.StartDrag(fieldDrag);
            _audioSource.PlayOneShot(_startDragSound);
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
                Create(item.SquadPlan, startCell, item.CreateUpgraded);
            }

            ResetPlayButton();
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
                _audioSource.PlayOneShot(_menuPlaceSound);
            }

            ResetPlayButton();
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