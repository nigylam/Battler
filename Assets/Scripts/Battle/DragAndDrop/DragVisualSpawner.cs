using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Battler.Battle.DragAndDrop
{
    public class DragVisualSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _uiDragContainer;
        [SerializeField] private Transform _worldContainer;

        public DragVisual Spawn(SquadPlan plan)
        {
            Image icon = Instantiate(plan.DragIcon, _uiDragContainer);
            SquadPreview preview = Instantiate(plan.Preview, _worldContainer);
            icon.gameObject.SetActive(false);
            preview.gameObject.SetActive(false);

            return new DragVisual { Icon = icon, Preview = preview };
        }

        public void Despawn(DragVisual visuals)
        {
            if (visuals.Icon != null) Destroy(visuals.Icon.gameObject);
            if (visuals.Preview != null) Destroy(visuals.Preview.gameObject);
        }
    }
}
