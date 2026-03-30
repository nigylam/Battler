using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas _parrentCanvas;
    [SerializeField] private Image _image;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private Camera _camera;
    [SerializeField] private SquadPreview _preview;
    [SerializeField] private SquadPlan _squad;
    [SerializeField] private SquadCreator _creator;
    [SerializeField] private Army _army;

    private Vector3 _startPosition;
    private (int x, int y) _startCell;
    private Transform _parrent;
    private bool _canBuild;

    public void OnBeginDrag(PointerEventData eventData)
    {
        _startPosition = transform.position;
        _parrent = transform.parent;
        _image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (IsPointerOverUI(eventData))
        {
            HandleUIDrag(eventData);
            return;
        }

        if (IsOnWorld(eventData.position, out Vector3 worldPoint, out Cell cell))
        {
            HandleWorldDrag(worldPoint, cell);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_canBuild)
        {
            if (_creator.TryCreate(_squad, _startCell, _army.transform, out Squad squad))
                _army.AddSquad(squad);
        }

        _preview.gameObject.SetActive(false);
        _image.enabled = true;
        _image.raycastTarget = true;
        transform.position = _startPosition;
        transform.SetParent(_parrent);
    }

    private void HandleWorldDrag(Vector3 previewPosition, Cell cell)
    {
        _image.enabled = false;
        _preview.gameObject.SetActive(true);

        if (cell != null)
        {
            if (cell.Field.HavePlace((cell.X, cell.Y), _squad.Size))
            {
                _preview.SetAvailable();
                _preview.transform.position = cell.transform.position;
                _startCell = (cell.X, cell.Y);
                _canBuild = true;
                return;
            }
        }

        _preview.SetBlocked();
        _canBuild = false;
        _preview.transform.position = previewPosition;
    }

    private bool IsPointerOverUI(PointerEventData eventData)
    {
        return EventSystem.current.IsPointerOverGameObject(eventData.pointerId);
    }

    private void HandleUIDrag(PointerEventData eventData)
    {
        _image.enabled = true;
        _preview.gameObject.SetActive(false);
        transform.position = eventData.position;
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
}
