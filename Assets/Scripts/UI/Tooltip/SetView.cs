using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Battler.UI.Tooltip
{
    public class SetView : MonoBehaviour
    {
        [SerializeField] private Image[] _icons;
        [SerializeField] private Sprite _filledSprite;
        [SerializeField] private Sprite _emptySprite;

        public void Initialize((int x, int y) size)
        {
            int width = 3;

            for(int i = 0; i < _icons.Length; i++) 
            {
                int x = i % width;
                int y = i / width;

                bool isWithinRange = x < size.x && y < size.y;

                if (isWithinRange)
                    _icons[i].sprite = _filledSprite;
                else
                    _icons[i].sprite = _emptySprite;
            }
        }
    }
}
