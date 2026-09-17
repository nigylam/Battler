using Battler.BattleSystem.DragAndDrop;
using Battler.UI.SquadView;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Battler.UI.BattleView
{
    public class DragItem : SquadItem<BattleSquadCell>, IDragable, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private GameObject _upgradeMark;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _hoverSound;
        [SerializeField] private float _hoverScale;

        public event Action<DragItem> DragStarted;
        public event Action<PointerEventData> Dragged;
        public event Action<PointerEventData> DragEnded;

        public bool CreateUpgraded { get; private set; }

        private bool _isDragging = false;

        public override void Initialize(BattleSquadCell squadCell)
        {
            base.Initialize(squadCell);
            CreateUpgraded = squadCell.CreateUpgraded;
            _upgradeMark.SetActive(CreateUpgraded);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (IsInteractable == false)
                return;

            DragStarted?.Invoke(this);
            transform.localScale = Vector3.one;
            _isDragging = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (IsInteractable == false && _isDragging == false)
                return;

            Dragged?.Invoke(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (IsInteractable == false && _isDragging == false)
                return;

            DragEnded?.Invoke(eventData);
            _isDragging = false;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if(IsInteractable == false) 
                return;

            transform.localScale = Vector3.one * _hoverScale;
            _audioSource.PlayOneShot(_hoverSound);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.localScale = Vector3.one;
        }
    }
}