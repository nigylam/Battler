using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private TextCounter _textCounter;
    [SerializeField] private Image _icon;
    
    private SquadPlan _squad;
    private ItemCounter _itemsCount;
    private SquadPreview _preview;
    private Image _dragImage;

    public event Action<DragItem> DragStarted;
    public event Action<PointerEventData> DragEnded;
    public event Action<PointerEventData> Drag;

    public SquadPlan Squad => _squad;

    private void OnEnable()
    {
        _itemsCount.Ended += OnItemsEnded;
    }

    private void OnDisable()
    {
        _textCounter.Disable();
        _itemsCount.Ended -= OnItemsEnded;
    }

    public void Initialize(SquadPlan plan, int count)
    {
        _squad = plan;
        _preview = Instantiate(_squad.Preview, transform);
        _dragImage = Instantiate(_squad.Image, transform);
        _icon.color = _squad.CellIcon.color;
        _itemsCount = new ItemCounter(count);
        _textCounter.Initialize(_itemsCount);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        DragStarted?.Invoke(this);
        _squad.Image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Drag?.Invoke(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragEnded?.Invoke(eventData);
        _preview.gameObject.SetActive(false);
        _dragImage.enabled = false;
        _dragImage.raycastTarget = false;
        _dragImage.transform.position = transform.position;
    }

    public void HandleUIDrag(PointerEventData eventData)
    {
        _dragImage.enabled = true;
        _preview.gameObject.SetActive(false);
        _dragImage.transform.position = eventData.position;
    }

    public void HandleWorldDrag()
    {
        _dragImage.enabled = false;
        _preview.gameObject.SetActive(true);
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

    public void Decrease()
    {
        _itemsCount.Decrease();
    }

    private void OnItemsEnded()
    {
        gameObject.SetActive(false);
    }
}