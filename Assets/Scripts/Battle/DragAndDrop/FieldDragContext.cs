using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Battler.Battle.DragAndDrop
{
    public class FieldDragContext : IPlacementDrag, IDisposable
    {
        private readonly UnitDragger _unitDragger;
        private readonly DragVisual _visual;
        private readonly DragVisualSpawner _spawner;

        public FieldDragContext(SquadContext squad, UnitDragger dragger, DragVisual visual, DragVisualSpawner visualSpawner)
        {
            Context = squad;
            Squad = squad.Plan;
            _unitDragger = dragger;
            _visual = visual;
            _spawner = visualSpawner;
            Subscribe();
        }

        public SquadContext Context { get; }
        public SquadPlan Squad { get; }

        public event Action<PointerEventData> Dragged;
        public event Action<PointerEventData> DragEnded;

        public void Dispose()
        {
            _spawner.Despawn(_visual);
            Unsubscribe();
        }

        public void CancelPlacement()
        {
            Dispose();
        }

        public void ConfirmPlacement()
        {
            Dispose();
        }

        public void HandleBuildAvailable(Vector3 position)
        {
            _visual.Preview.SetAvailable();
            _visual.Preview.transform.position = position;
        }

        public void HandleBuildBlocked(Vector3 position)
        {
            _visual.Preview.SetBlocked();
            _visual.Preview.transform.position = position;
        }

        public void HandleUIDrag(PointerEventData eventData)
        {
            _visual.Preview.gameObject.SetActive(false);
            _visual.Icon.gameObject.SetActive(true);
            _visual.Icon.transform.position = eventData.position;
        }

        public void HandleWorldDrag()
        {
            _visual.Icon.gameObject.SetActive(false);
            _visual.Preview.gameObject.SetActive(true);
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
        }

        private void OnDrag(PointerEventData data)
        {
            Dragged?.Invoke(data);
        }
    }
}
