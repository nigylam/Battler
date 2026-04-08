using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SquadPlacer : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private LayerMask _thisCanvas;
    [SerializeField] private DragItem _itemPrefab;

    private List<DragItem> _dragItems = new();
    private DragItem _draggingItem;
    private bool _canBuild;
    private (int x, int y) _startCell;

    private bool _subscribed;

    public event Action<SquadPlan, (int x, int y)> ReadyForBuild;

    private void OnEnable()
    {
        if (_subscribed)
            return;

        foreach (var item in _dragItems)
        {
            item.DragStarted += OnDragStarted;
            item.DragEnded += OnDragEnded;
            item.Drag += OnDrag;
        }
    }

    private void OnDisable()
    {
        foreach (var item in _dragItems)
        {
            item.DragStarted -= OnDragStarted;
            item.DragEnded -= OnDragEnded;
            item.Drag -= OnDrag;
        }

        _subscribed = false;
    }

    public void SetSquads(SquadKeeper squadKeeper)
    {
        foreach (SquadPlan squad in squadKeeper.Squads)
        {
            DragItem item = Instantiate(_itemPrefab, transform);
            item.Initialize(squad, squadKeeper.GetSquadsCount(squad));
            _dragItems.Add(item);
            item.DragStarted += OnDragStarted;
            item.DragEnded += OnDragEnded;
            item.Drag += OnDrag;
        }

        _subscribed = true;
    }

    public void ClearSquads()
    {
        foreach (var item in _dragItems)
        {
            item.DragStarted -= OnDragStarted;
            item.DragEnded -= OnDragEnded;
            item.Drag -= OnDrag;
            Destroy(item.gameObject);
        }
    }

    private void OnDragStarted(DragItem item)
    {
        _draggingItem = item;
    }

    private void OnDrag(PointerEventData eventData)
    {
        if (IsPointerOverUI(eventData))
        {
            _canBuild = false;
            _draggingItem.HandleUIDrag(eventData);
            return;
        }

        if (IsOnWorld(eventData.position, out Vector3 worldPoint, out Cell cell))
        {
            _draggingItem.HandleWorldDrag();

            if (cell != null && cell.Field.HavePlace((cell.X, cell.Y), _draggingItem.Squad.Size))
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
        if (_canBuild)
        {
            ReadyForBuild?.Invoke(_draggingItem.Squad, _startCell);
            _draggingItem.Decrease();
        }

        _draggingItem = null;
    }

    private bool IsPointerOverUI(PointerEventData eventData)
    {
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (var result in results)
        {
            if(IsInLayerMask(result.gameObject))
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
        return (_thisCanvas.value & (1 << obj.layer)) != 0;
    }
}
