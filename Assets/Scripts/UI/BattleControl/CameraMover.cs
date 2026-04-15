using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMover : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private Transform _camera;
    [SerializeField] private float _dragSensitivity;
    [SerializeField] private float _edgePannelSpeed;
    [SerializeField] private EdgePannel _topEdgePannel;
    [SerializeField] private EdgePannel _bottomEdgePannel;

    private bool _isDragging = false;
    private float _currentEdgeDirection = 0f;
    private float _topEdgeDirection = 1f;
    private float _bottomEdgeDirection = -1f;

    private void OnEnable()
    {
        _topEdgePannel.PointerEnter += OnTopEdgePointerEnter;
        _bottomEdgePannel.PointerEnter += OnBottomEdgePointerEnter;
        _topEdgePannel.PointerExit += OnEdgePointerExit;
        _bottomEdgePannel.PointerExit += OnEdgePointerExit;
    }

    private void OnDisable()
    {
        _topEdgePannel.PointerEnter -= OnTopEdgePointerEnter;
        _bottomEdgePannel.PointerEnter -= OnBottomEdgePointerEnter;
        _topEdgePannel.PointerExit -= OnEdgePointerExit;
        _bottomEdgePannel.PointerExit -= OnEdgePointerExit;
    }

    private void OnTopEdgePointerEnter()
    {
        _currentEdgeDirection = _topEdgeDirection;
    }

    private void OnBottomEdgePointerEnter()
    {
        _currentEdgeDirection = _bottomEdgeDirection;
    }

    private void OnEdgePointerExit()
    {
        _currentEdgeDirection = 0;
    }

    private void Update()
    {
        if (_isDragging == false && _currentEdgeDirection != 0f)
        {
            Vector3 moveDirection = new(0, 0, _currentEdgeDirection);
            _camera.Translate(moveDirection * _edgePannelSpeed * Time.deltaTime, Space.World);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isDragging = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isDragging = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 move = new Vector3(0, 0, -eventData.delta.y * _dragSensitivity);
        _camera.Translate(move, Space.World);
    }

    public void StartEdgePan(float directionZ)
    {
        _currentEdgeDirection = directionZ;
    }

    public void StopEdgePan()
    {
        _currentEdgeDirection = 0f;
    }
}
