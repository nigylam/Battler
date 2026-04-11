using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SquadPlacer
{
    private BeforeBattleMenu _beforeBattleMenu;
    private Camera _camera;
    private LayerMask _groundMask;
    private LayerMask _thisCanvas;
    private DragItem _draggingItem;
    private bool _canBuild;
    private (int x, int y) _startCell;

    public event Action PlayButtonClicked;
    public event Action<SquadPlan, (int x, int y)> Place;

    public SquadPlacer
    (
        BeforeBattleMenu beforeBattleMenu, 
        Camera camera, 
        LayerMask groundMask, 
        LayerMask thisCanvas
    )
    {
        _beforeBattleMenu = beforeBattleMenu;
        _camera = camera;
        _groundMask = groundMask;
        _thisCanvas = thisCanvas;
    }

    public void SetSquads(SquadKeeper squadKeeper)
    {
        _beforeBattleMenu.SetSquads(squadKeeper);
    }

    public void Enable()
    {
        _beforeBattleMenu.DragStarted += OnDragStarted;
        _beforeBattleMenu.PlayButtonClicked += OnPlayButtonClicked;
        _beforeBattleMenu.gameObject.SetActive(true);
    }

    public void Disable()
    {
        _beforeBattleMenu.DragStarted -= OnDragStarted;
        _beforeBattleMenu.PlayButtonClicked -= OnPlayButtonClicked;
        _beforeBattleMenu.gameObject.SetActive(false);

        if (_draggingItem != null)
        {
            _draggingItem.Drag -= OnDrag;
            _draggingItem.DragEnded -= OnDragEnded;
        }
    }

    private void OnDragStarted(DragItem item)
    {
        _draggingItem = item;
        _draggingItem.DragEnded += OnDragEnded;
        _draggingItem.Drag += OnDrag;
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
            Place?.Invoke(_draggingItem.Squad, _startCell);
            _draggingItem.Decrease();
        }

        _draggingItem.DragEnded -= OnDragEnded;
        _draggingItem.Drag -= OnDrag;
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
        return (_thisCanvas.value & (1 << obj.layer)) != 0;
    }

    private void OnPlayButtonClicked()
    {
        PlayButtonClicked?.Invoke();
    }
}