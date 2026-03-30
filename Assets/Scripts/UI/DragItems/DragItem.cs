using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private SquadItem _squad;

    private Vector3 _startPosition;

    public event Action<DragItem> DragStarted;
    public event Action<PointerEventData> DragEnded;
    public event Action<PointerEventData> Drag;

    public SquadPlan Squad => _squad.Plan;

    public void OnBeginDrag(PointerEventData eventData)
    {
        DragStarted?.Invoke(this);

        _startPosition = transform.position;
        _squad.Image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Drag?.Invoke(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragEnded?.Invoke(eventData);
        _squad.Preview.gameObject.SetActive(false);
        _squad.Image.enabled = true;
        _squad.Image.raycastTarget = true;
        transform.position = _startPosition;
    }

    public void HandleUIDrag(PointerEventData eventData)
    {
        _squad.Image.enabled = true;
        _squad.Preview.gameObject.SetActive(false);
        transform.position = eventData.position;
    }

    public void HandleWorldDrag()
    {
        _squad.Image.enabled = false;
        _squad.Preview.gameObject.SetActive(true);
    }

    public void HandleBuildAvailable(Vector3 position)
    {
        _squad.Preview.SetAvailable();
        _squad.Preview.transform.position = position;
    }

    public void HandleBuildBlocked(Vector3 position)
    {
        _squad.Preview.SetBlocked();
        _squad.Preview.transform.position = position;
    }
}
