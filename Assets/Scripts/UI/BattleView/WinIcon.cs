using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Battler.UI.BattleView
{
    public class WinIcon : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Color _complete;
        [SerializeField] private Color _default;

        private void OnEnable()
        {
            SetDefault();
        }

        public void SetComplete()
        {
            _image.color = _complete;
        }

        public void SetDefault()
        {
            _image.color = _default;
        }
    }
}
