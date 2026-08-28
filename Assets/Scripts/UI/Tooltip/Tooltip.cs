using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battler.UI.Tooltip
{
    public abstract class Tooltip : MonoBehaviour
    {
        private const float PivotRight = 0f;
        private const float PivotLeft = 1f;

        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private Vector2 _offset;

        public void Enable(Vector2 position)
        {
            Vector2 dynamicOffset = _offset;

            if (position.x > Screen.width / 2)
            {
                dynamicOffset.x = -_offset.x;
                _rectTransform.pivot = new Vector2(PivotLeft, _rectTransform.pivot.y);
            }
            else
            {
                _rectTransform.pivot = new Vector2(PivotRight, _rectTransform.pivot.y);
            }

            transform.position = position + dynamicOffset;
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}
