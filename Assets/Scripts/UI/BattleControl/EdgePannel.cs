using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class EdgePannel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public event Action PointerEnter;
    public event Action PointerExit;

    public void OnPointerEnter(PointerEventData eventData)
    {
        PointerEnter?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PointerExit?.Invoke();
    }
}
