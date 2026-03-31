using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SquadPlacer : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private LayerMask _thisCanvas;
    [SerializeField] private SquadCreator _creator;
    [SerializeField] private Army _army;
    [SerializeField] private DragItem[] _dragItems;

    private DragItem _draggingItem;
    private bool _canBuild;
    private (int x, int y) _startCell;

    private void OnEnable()
    {
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
            if (_creator.TryCreate(_draggingItem.Squad, _startCell, _army.transform, out Squad squad))
                _army.AddSquad(squad);

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
