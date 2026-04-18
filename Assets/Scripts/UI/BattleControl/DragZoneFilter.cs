using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battler
{
    public class DragZoneFilter : MonoBehaviour, ICanvasRaycastFilter
    {
        [SerializeField] private LayerMask _unitLayer;
        [SerializeField] private Camera _ñamera;

        public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
        {
            Ray ray = _ñamera.ScreenPointToRay(sp);

            if (Physics.Raycast(ray, 100f, _unitLayer))
                return false;

            return true;
        }
    }
}
