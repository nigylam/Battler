using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragItem : Item, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private TextCounter _textCounter;

    private ItemCounter _itemsCount;
    private SquadPreview _preview;
    private Image _dragImage;

    public event Action<DragItem> DragStarted;
    public event Action<PointerEventData> DragEnded;
    public event Action<PointerEventData> Drag;

    private void OnEnable()
    {
        _itemsCount.Ended += OnItemsEnded;
    }

    private void OnDisable()
    {
        _textCounter.Disable();
        _itemsCount.Ended -= OnItemsEnded;
    }

    public void Initialize(SquadPlan squad, int count) 
    {
        base.Initialize(squad);
        _preview = Instantiate(squad.Preview, transform);
        _dragImage = Instantiate(squad.Image, transform);
        _itemsCount = new ItemCounter(count);
        _textCounter.Initialize(_itemsCount);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        DragStarted?.Invoke(this);
        _dragImage.raycastTarget = false;
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