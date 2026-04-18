using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Battler.Battle.DragAndDrop;
using UnityEngine.UI;

namespace Battler
{
    public class FieldDragContext : IPlacementDrag, IDisposable
    {
        private readonly UnitDragger _unitDragger;
        private readonly Image _squadIcon;
        private readonly SquadPreview _preview;

        private bool _isOverUI;

        public FieldDragContext(SquadContext squad, UnitDragger dragger, Image squadIcon, SquadPreview preview)
        {
            Context = squad;
            Squad = squad.Plan;
            _unitDragger = dragger;
            Subscribe();
            _squadIcon = squadIcon;
            _preview = preview;
        }

        public SquadContext Context { get; }
        public SquadPlan Squad { get; }

        public event Action<PointerEventData> Dragged;
        public event Action<PointerEventData> DragEnded;
        public event Action<FieldDragContext> AddItem;
        public event Action<FieldDragContext> MoveSquad;
        public event Action<FieldDragContext> CanceDrag;

        public void Dispose()
        {
            _squadIcon.gameObject.SetActive(false);
            _preview.gameObject.SetActive(false);
            Unsubscribe();
        }

        public void CancelPlacement()
        {
            Dispose();

            if (_isOverUI)
                AddItem?.Invoke(this);
            else
                CanceDrag?.Invoke(this);
        }

        public void ConfirmPlacement()
        {
            Dispose();
            MoveSquad?.Invoke(this);
        }

        public void HandleBuildAvailable(Vector3 position)
        {
            _preview.SetAvailable();
            _preview.transform.position = position;
        }

        public void HandleBuildBlocked(Vector3 position)
        {
            _preview.SetBlocked();
            _preview.transform.position = position;
        }

        public void HandleUIDrag(PointerEventData eventData)
        {
            _isOverUI = true;
            _squadIcon.gameObject.SetActive(true);
            _squadIcon.transform.position = eventData.position;
        }

        public void HandleWorldDrag()
        {
            _isOverUI = false;
            _preview.gameObject.SetActive(true);
        }

        private void Subscribe()
        {
            _unitDragger.Dragged += OnDrag;
            _unitDragger.DragEnded += OnDragEnd;
        }

        private void Unsubscribe()
        {
            _unitDragger.Dragged -= OnDrag;
            _unitDragger.DragEnded -= OnDragEnd;
        }

        private void OnDragEnd(PointerEventData data)
        {
            DragEnded?.Invoke(data);
            Unsubscribe();
        }

        private void OnDrag(PointerEventData data)
        {
            Dragged?.Invoke(data);
        }
    }
}
