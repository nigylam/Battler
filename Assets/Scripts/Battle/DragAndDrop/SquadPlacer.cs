using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Battler.Battle.DragAndDrop
{
    public class SquadPlacer 
    {
        private Camera _camera;
        private LayerMask _groundMask;
        private LayerMask _armyPannel;
        private IPlacementDrag _draggingItem;
        private (int x, int y) _startCell;
        private bool _canBuild;
        private bool _isOverUI;

        public event Action<IPlacementDrag, (int x, int y)> DropSuccess;
        public event Action<IPlacementDrag> DropToUI;
        public event Action<IPlacementDrag> DropFail;

        public SquadPlacer (Camera camera,
            LayerMask groundMask,
            LayerMask thisCanvas
        )
        {
            _camera = camera;
            _groundMask = groundMask;
            _armyPannel = thisCanvas;
        }

        public void StartDrag(IPlacementDrag item)
        {
            _draggingItem = item;
            _draggingItem.Dragged += OnDrag;
            _draggingItem.DragEnded += OnDragEnded;
        }

        private void OnDrag(PointerEventData eventData)
        {
            _isOverUI = IsPointerOverUI(eventData);

            if (_isOverUI)
            {
                _canBuild = false;
                _draggingItem.HandleUIDrag(eventData);
                return;
            }

            if (IsOnWorld(eventData.position, out Vector3 worldPoint, out Cell cell))
            {
                _draggingItem.HandleWorldDrag();

                if (cell != null && cell.Field.HavePlace((cell.X, cell.Y), _draggingItem.SquadPlan.Size))
                {
                    _draggingItem.HandleBuildAvailable(cell.transform.position);
                    _startCell = (cell.X, cell.Y);
                    _canBuild = true;
                    return;
                }

                _canBuild = false;
                _draggingItem.HandleBuildBlocked(worldPoint);
            }
        }

        private void OnDragEnded(PointerEventData eventData)
        {
            if (_isOverUI)
            {
                DropToUI?.Invoke(_draggingItem);
            }
            else if (_canBuild)
            {
                DropSuccess?.Invoke(_draggingItem, _startCell);
            }
            else
            {
                DropFail?.Invoke(_draggingItem);
            }

            _draggingItem.Dragged -= OnDragEnded;
            _draggingItem.DragEnded -= OnDrag;
            _draggingItem = null;
        }

        private bool IsPointerOverUI(PointerEventData eventData)
        {
            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);

            foreach (var result in results)
            {
                if (IsInLayerMask(result.gameObject))
                    return true;
            }

            return false;
        }

        private bool IsOnWorld(Vector3 position, out Vector3 worldPoint, out Cell cell)
        {
            cell = null;
            worldPoint = Vector3.zero;
            Ray ray = _camera.ScreenPointToRay(position);
            float distance = 1000f;

            if (Physics.Raycast(ray, out RaycastHit hit, distance, _groundMask) == false)
                return false;

            worldPoint = hit.point;
            hit.collider.TryGetComponent(out cell);

            return true;
        }

        private bool IsInLayerMask(GameObject obj)
        {
            return (_armyPannel.value & 1 << obj.layer) != 0;
        }
    }
}