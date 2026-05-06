using Battler.BattleSystem.Squads;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Battler.BattleSystem.DragAndDrop
{
    public abstract class DragContext
    {
        private readonly DragVisual _visual;
        private readonly DragVisualSpawner _spawner;
        private readonly IDragable _dragable;

        public DragContext(SquadPlan squadPlan, bool createUpgraded, DragVisual visual, DragVisualSpawner visualSpawner, IDragable dragable)
        {
            SquadPlan = squadPlan;
            CreateUpgraded = createUpgraded;
            _visual = visual;
            _spawner = visualSpawner;
            _dragable = dragable;
            Subscribe();
        }

        public event Action<PointerEventData> Dragged;
        public event Action<PointerEventData> DragEnded;

        public SquadPlan SquadPlan { get; }
        public bool CreateUpgraded { get; }

        public void Dispose()
        {
            Unsubscribe();
            _spawner.Despawn(_visual);
        }

        public void HandleBuildAvailable(Vector3 position)
        {
            _visual.Preview.SetAvailable();
            _visual.Preview.transform.position = position;
        }

        public void HandleBuildBlocked(Vector3 position)
        {
            _visual.Preview.SetBlocked();
            _visual.Preview.transform.position = position;
        }

        public void HandleUIDrag(PointerEventData eventData)
        {
            _visual.Preview.gameObject.SetActive(false);
            _visual.Icon.enabled = true;
            _visual.Icon.transform.position = eventData.position;
        }

        public void HandleWorldDrag()
        {
            _visual.Icon.enabled = false;
            _visual.Preview.gameObject.SetActive(true);
        }

        private void Subscribe()
        {
            _dragable.Dragged += OnDrag;
            _dragable.DragEnded += OnDragEnd;
        }

        private void Unsubscribe()
        {
            _dragable.Dragged -= OnDrag;
            _dragable.DragEnded -= OnDragEnd;
        }

        private void OnDragEnd(PointerEventData data)
        {
            DragEnded?.Invoke(data);
        }

        private void OnDrag(PointerEventData data)
        {
            Dragged?.Invoke(data);
        }
    }
}
