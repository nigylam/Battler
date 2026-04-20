using Battler.UI.BattleView;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Battler.Battle.DragAndDrop
{
    public class MenuDragContext : IPlacementDrag
    {
        public SquadPlan Squad { get; }

        private DragItem _sourceItem;
        private DragVisual _visuals;
        private DragVisualSpawner _spawner;

        public event Action<PointerEventData> Dragged;
        public event Action<PointerEventData> DragEnded;

        public MenuDragContext(DragItem sourceItem, DragVisual visuals, DragVisualSpawner spawner)
        {
            _sourceItem = sourceItem;
            Squad = sourceItem.Squad;
            _visuals = visuals;
            _spawner = spawner;

            _sourceItem.Dragged += OnDragged;
            _sourceItem.DragEnded += OnDragEnded;
        }

        private void OnDragged(PointerEventData data) => Dragged?.Invoke(data);
        private void OnDragEnded(PointerEventData data) => DragEnded?.Invoke(data);

        public void HandleUIDrag(PointerEventData eventData)
        {
            _visuals.Preview.gameObject.SetActive(false);
            _visuals.Icon.gameObject.SetActive(true);
            _visuals.Icon.transform.position = eventData.position;
        }

        public void HandleWorldDrag()
        {
            _visuals.Icon.gameObject.SetActive(false);
            _visuals.Preview.gameObject.SetActive(true);
        }

        public void HandleBuildAvailable(Vector3 position)
        {
            _visuals.Preview.SetAvailable();
            _visuals.Preview.transform.position = position;
        }

        public void HandleBuildBlocked(Vector3 position)
        {
            _visuals.Preview.SetBlocked();
            _visuals.Preview.transform.position = position;
        }

        public void ConfirmPlacement()
        {
            _sourceItem.Decrease();
            Cleanup();
        }

        public void CancelPlacement()
        {
            Cleanup();
        }

        private void Cleanup()
        {
            _sourceItem.Dragged -= OnDragged;
            _sourceItem.DragEnded -= OnDragEnded;
            _spawner.Despawn(_visuals);
        }
    }
}
