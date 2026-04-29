using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Battler.Battle.DragAndDrop
{
    public class DragVisualSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _uiDragContainer;
        [SerializeField] private Transform _worldContainer;
        [SerializeField] private Image _dragImage;

        public DragVisual Spawn(SquadPlan plan)
        {
            Image icon = Instantiate(_dragImage, _uiDragContainer);
            icon.sprite = plan.DragIcon;
            SquadPreview preview = Instantiate(plan.Preview, _worldContainer);
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
