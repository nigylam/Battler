using Battler.Battle.DragAndDrop;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Battler.UI.BattleView
{
    public class DragItem : SquadItem<BattleSquadCell>, IDragable, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [SerializeField] private GameObject _upgradeMark;

        public event Action<DragItem> DragStarted;
        public event Action<PointerEventData> Dragged;
        public event Action<PointerEventData> DragEnded;

        public bool CreateUpgraded { get; private set; }

        public override void Initialize(BattleSquadCell squadCell)
        {
            base.Initialize(squadCell);
            CreateUpgraded = squadCell.CreateUpgraded;
            _upgradeMark.SetActive(CreateUpgraded);
        }

        public void OnBeginDrag(PointerEventData eventData) => DragStarted?.Invoke(this);
        public void OnDrag(PointerEventData eventData) => Dragged?.Invoke(eventData);
        public void OnEndDrag(PointerEventData eventData) => DragEnded?.Invoke(eventData);
    }
}