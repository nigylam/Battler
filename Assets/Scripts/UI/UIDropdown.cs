using Battler.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Battler
{
    public class UIDropdown : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _hoverSound;
        [SerializeField] private AudioClip _clickSound;
        [SerializeField] private List<PointerEventHandler> _handlers;

        private void OnEnable()
        {
            foreach (var handler in _handlers)
            {
                handler.Clicked += OnClick;
                handler.Hovered += OnHover;
            }
        }

        private void OnDisable()
        {
            foreach (var handler in _handlers)
            {
                handler.Clicked -= OnClick;
                handler.Hovered -= OnHover;
            }
        }

        void Awake()
        {
            _audioSource.ignoreListenerPause = true;
        }

        public void OnClick()
        {
            if (_clickSound != null)
                _audioSource.PlayOneShot(_clickSound);
        }

        public void OnHover()
        {
            _audioSource.PlayOneShot(_hoverSound);
        }
    }
}
